using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using CFinder.Application.Models.Settings;
using CFinder.Application.Utils;
using CFinder.Domain.Log;
using CFinder.Domain.Settings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;

namespace CFinder.Application.Models.CryptoFinderModels;

public class Metamask : BaseWallet
{
    public override Wallet.WalletType walletType { get; set; } = Wallet.WalletType.Metamask;

    public Metamask(string directory) : base(directory)
    {
        
    }

    public override async Task<string> GetEncrypted(DecryptorSettingsDto settings)
    {
        if (!System.IO.Directory.Exists(base.Directory))
            return string.Empty;
        
        var logFile = System.IO.Directory.GetFiles(base.Directory, "*", SearchOption.AllDirectories)
            .FirstOrDefault(x => x.EndsWith(".log", StringComparison.OrdinalIgnoreCase));
        if (logFile == null)
            return string.Empty;
        
        var logText = await File.ReadAllTextAsync(logFile);

        string vaultString = string.Empty;
        if (settings.EncryptedParsingType == EncryptedParsingType.Cycle)
        {
            vaultString = await JsonSearcher.SearchCycle(logText, @"{\""data\"":", settings.CycleItterationCount);
        }
        else
        {
            vaultString = await JsonSearcher.SearchRegex(logText,
                @"{""vault"":""([0-9a-zA-z+.""={},'/:]+)""},""[\w]+Controller""");
        }
        
        return vaultString;
    }
    public override async Task<string> GetHashcat(string encryptedJson)
    {
        await Task.CompletedTask;
        if (!string.IsNullOrWhiteSpace(encryptedJson))
        {
            var parsedDocument = JsonDocument.Parse(encryptedJson).RootElement;

            return $"$metamask$" +
                   $"{parsedDocument.GetProperty("salt").GetString()}$" +
                   $"{parsedDocument.GetProperty("iv").GetString()}$" +
                   $"{parsedDocument.GetProperty("data")}";
        }

        return string.Empty;
    }
    public override async Task<string[]> GetOriginalAddresses()
    {
        await Task.CompletedTask;
        if (!System.IO.Directory.Exists(base.Directory))
            return Array.Empty<string>();
        
        var logFile = System.IO.Directory.GetFiles(base.Directory)
            .FirstOrDefault(x => x.EndsWith(".log", StringComparison.OrdinalIgnoreCase));
        
        if (logFile == null)
            return Array.Empty<string>();

        var logText = await File.ReadAllTextAsync(logFile);
        var addressesString = await  JsonSearcher.SearchCycle(logText, @"{""cachedBalances"":", 500);
        
        if (!string.IsNullOrWhiteSpace(addressesString) && Regex.IsMatch(addressesString, @"(0x[a-fA-F0-9]{40})"))
        {
            var addresses = Regex.Matches(addressesString, @"(0x[a-fA-F0-9]{40})")
                .Select(x => x.Groups[1].Value)
                .Distinct()
                .ToArray();

            return addresses;
        }

        return Array.Empty<string>();
        
    }
    public override async Task<(string decrypted, string password, string mnemonic)> Decrypt(string encryptedJson,
        string?[] passwordList)
    {
        await Task.CompletedTask;
        if (string.IsNullOrWhiteSpace(encryptedJson) || passwordList.Length == 0)
        {
            return (string.Empty, string.Empty, string.Empty);
        }
        
        var parsedDocument = JsonDocument.Parse(encryptedJson).RootElement;
        string ivString = parsedDocument.GetProperty("iv").GetString() ?? "";
        string saltString = parsedDocument.GetProperty("salt").GetString() ?? "";
        string dataString = parsedDocument.GetProperty("data").GetString() ?? "";
        

        byte[] iv, salt, data;
        iv = Convert.FromBase64String(ivString);
        salt = Convert.FromBase64String(saltString);
        data = Convert.FromBase64String(dataString);

        string decrypted = string.Empty;
        string validPassword = string.Empty;
        string? newMnemonic = "";
        
        Parallel.ForEach(passwordList, (password, state) =>
        {
            try
            {
                byte[] key = PBKDF2.DeriveKey(password, salt);
                GcmBlockCipher gcmBlockCipher = new GcmBlockCipher(new AesEngine());
                AeadParameters parameters = new AeadParameters(new KeyParameter(key), 128, iv, null);
                gcmBlockCipher.Init(false, parameters);
                byte[] plainBytes = new byte[gcmBlockCipher.GetOutputSize(data.Length)];
                int retLen = gcmBlockCipher.ProcessBytes(data, 0, data.Length, plainBytes, 0);
                gcmBlockCipher.DoFinal(plainBytes, retLen);
                
                decrypted = Encoding.UTF8.GetString(plainBytes);
                validPassword = password;
                
                state.Stop();
            }
            catch
            {
                // ignored
            }
        });
        
        if(string.IsNullOrWhiteSpace(decrypted))
            return (string.Empty, string.Empty, string.Empty);
        
        var parsedDecrypted = JsonDocument.Parse(decrypted).RootElement;
        var mnemonicElement = JsonSearcher.Search(parsedDecrypted, "mnemonic");
        
        if (mnemonicElement.ValueKind == JsonValueKind.Array)
        {
            var byteArray = mnemonicElement.EnumerateArray()
                .Select(x => (byte)x.GetInt32())
                .ToArray();
            newMnemonic = Encoding.UTF8.GetString(byteArray);
        }
        else if(mnemonicElement.ValueKind == JsonValueKind.String)
        {
            newMnemonic = mnemonicElement.GetString();
        }

        return (decrypted, validPassword, newMnemonic);
    }
}