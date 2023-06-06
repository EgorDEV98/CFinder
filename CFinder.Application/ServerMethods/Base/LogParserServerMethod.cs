using System.Text.RegularExpressions;
using CFinder.Application.Models.CryptoFinderModels;
using CFinder.Application.Models.Result;
using CFinder.Domain.Log;

namespace CFinder.Application.ServerMethods.Base;

public class LogParserServerMethod
{
    
    /// <summary>
    /// Искать в папке с логом файл Password.txt и распарсить его
    /// </summary>
    /// <param name="directory">Папка с логом</param>
    /// <returns>List AuthenticationDto</returns>
    public virtual async Task<List<AuthenticationDto>> GetAuthentication(string directory)
    {
        await Task.CompletedTask;

        var authList = new List<AuthenticationDto>();

        if (!Directory.Exists(directory))
            return authList;
        
        var passwordFile = Directory
            .GetFiles(directory, "*", SearchOption.AllDirectories)
            .FirstOrDefault(x => x.Contains("password", StringComparison.OrdinalIgnoreCase));
        if (passwordFile == null)
            return authList;

        return await ParsePasswordFile(passwordFile);
    }
    
    /// <summary>
    /// Получить список кошельков
    /// </summary>
    /// <param name="directory">Папка с логом</param>
    /// <returns></returns>
    public virtual async Task<List<BaseWallet>> GetWalelts(string directory)
    {
        var walletList = new List<BaseWallet>();
        var walletDirectory = GetWalletDirectory(directory);
        if (!Directory.Exists(walletDirectory))
        {
            return walletList;
        }
        
        var walletsDirectories = System.IO.Directory.GetDirectories(walletDirectory);
        var walletNames = Enum.GetNames<Wallet.WalletType>();
        
        foreach (var walletsDirectory in walletsDirectories)
        {
            var foundWallet = walletNames
                .FirstOrDefault(term => walletsDirectory.Contains(term, StringComparison.OrdinalIgnoreCase));
            
            if(foundWallet == null)
                continue;
            
            var walletEnum = Enum.Parse<Wallet.WalletType>(foundWallet);

            switch (walletEnum)
            {
                case Wallet.WalletType.Metamask:
                    walletList.Add(new Metamask(walletsDirectory));
                    break;
            }
        }

        await Task.CompletedTask;
        return walletList;
    }
    
    
    
    
    
    
    /// <summary>
    /// Парсить файл с паролем
    /// </summary>
    /// <param name="passwordFile"></param>
    /// <returns></returns>
    private async Task<List<AuthenticationDto>> ParsePasswordFile(string passwordFile)
    {
        var authsList = new List<AuthenticationDto>();

        using (var streamReader = new StreamReader(passwordFile))
        {
            while (!streamReader.EndOfStream)
            {
                var auth = new AuthenticationDto();

                for (var i = 0; i < 4; i++)
                {
                    var line = await streamReader.ReadLineAsync();

                    if (line == null)
                        break;

                    var parts = line.Split(new[] { ':' }, 2);

                    if (parts.Length != 2)
                        continue;

                    var startString = parts[0].Trim();
                    var endString = parts[1].Trim();

                    if (startString.Contains("pass", StringComparison.OrdinalIgnoreCase))
                    {
                        auth.Password = endString;
                    }
                    else if (startString.Contains("url", StringComparison.OrdinalIgnoreCase) ||
                             startString.Contains("link", StringComparison.OrdinalIgnoreCase) ||
                             startString.Contains("host", StringComparison.OrdinalIgnoreCase))
                    {
                        auth.Url = endString;
                    }
                    else if (startString.Contains("user", StringComparison.OrdinalIgnoreCase) ||
                             startString.Contains("login", StringComparison.OrdinalIgnoreCase))
                    {
                        auth.Login = endString;
                    }
                    else if (startString.Contains("app", StringComparison.OrdinalIgnoreCase) ||
                             startString.Contains("soft", StringComparison.OrdinalIgnoreCase))
                    {
                        auth.Application = endString;
                    }
                }

                authsList.Add(auth);
            }
        }

        return authsList;
    }

    /// <summary>
    /// Получить директорую .../wallet
    /// </summary>
    /// <param name="directory">Папка с логом</param>
    /// <returns></returns>
    private string? GetWalletDirectory(string directory)
    {
        string? walletDirectory = null;
          
        var stringWithWallet = Directory
            .GetFiles(directory, "*", SearchOption.AllDirectories)
            .FirstOrDefault(x => x.Contains("allet", StringComparison.OrdinalIgnoreCase));
        if (stringWithWallet == null)
            return null;

        var splitDirectory = stringWithWallet.Split("\\");

        foreach (var splitElement in splitDirectory)
        {
            walletDirectory = Path.Combine(walletDirectory ?? "", splitElement);
            if(splitElement.Contains("wallet", StringComparison.OrdinalIgnoreCase))
                break;
        }

        return walletDirectory;
    }
}