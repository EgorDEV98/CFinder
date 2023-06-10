using CFinder.Application.Common.Exceptions;
using CFinder.Application.Models.Result;
using CFinder.Application.Models.Settings;
using CFinder.Application.Models.WorkHistory.DTOs;
using CFinder.Application.Repository.ResultRepository.Commands.CreateListResults;
using CFinder.Application.Repository.ResultRepository.Commands.CreateResult;
using CFinder.Application.Repository.SettingsRepository.Queries.GetActiveSettings;
using CFinder.Application.Repository.WorkHistoryRepository.Commands.CreateWorkHistory;
using CFinder.Application.Repository.WorkHistoryRepository.Commands.UpdateWorkHistory;
using CFinder.Application.ServerMethods.Base;
using CFinder.Application.ServerMethods.Override;
using CFinder.Domain.Settings;
using CFinder.Domain.WorkHistory;

namespace CFinder.Application.Services;

public class CryptoFinderService : BaseService
{
    private const string SERVICE_NAME = "Crypto Finder Service";
    
    private readonly LogParserServerMethod _parserServerMethod;
    private readonly AddressGeneratorServerMethod _addressGeneratorServerMethod;
    private readonly AnkrAddressCheckBalanceServerMethodOverride _balanceServerMethodOverride;
    
    private string[] Directorys { get; set; }
    private WorkHistoryDto WorkHistoryDto { get; set; }
    private UpdateWorkHistoryCommand? UpdateWorkHistoryCommand { get; set; }
    private SettingsDto? SettingsDto { get; set; }
    private long _itteration;
    
    public CryptoFinderService(
        IServiceProvider serviceProvider,
        LogParserServerMethod parserServerMethod,
        AddressGeneratorServerMethod addressGeneratorServerMethod,
        AnkrAddressCheckBalanceServerMethodOverride balanceServerMethodOverride) 
        : base(serviceProvider)
    {
        _parserServerMethod = parserServerMethod;
        _addressGeneratorServerMethod = addressGeneratorServerMethod;
        _balanceServerMethodOverride = balanceServerMethodOverride;
    }

    public async Task StartAsync(string path)
    {
        if (!Directory.Exists(path))
        {
            throw new NotFoundException(nameof(CryptoFinderService), path);
        }

        await FillDataAsync(path);
        
        var command = new CreateWorkHistoryCommand()
        {
            Name = SERVICE_NAME,
            Path = path,
            Total = Directorys.Length
        };
        WorkHistoryDto = await Mediator.Send(command);

        await StartAsync();
        try
        {
            UpdateWorkHistoryCommand = new UpdateWorkHistoryCommand()
            {
                Id = WorkHistoryDto.Id,
                Current = WorkHistoryDto.Current,
                Total = WorkHistoryDto.Total,
                Status = Status.Finished,
                EndDate = DateTime.Now
            };
            await Mediator.Send(UpdateWorkHistoryCommand);
            
        }
        catch
        {
            // ignored
        }
    }

    private async Task StartAsync()
    {
        var dataMemory = new List<LogDto>();
        foreach (var directory in Directorys)
        {
            try
            {
                ++WorkHistoryDto.Current;
                var parsedLog = await _parserServerMethod.FullParseLog(
                    directory, 
                    SettingsDto?.ParserSettings ?? 
                    SettingsDto.GetDefault().ParserSettings);
                var baseWallets = await _parserServerMethod.GetWalelts(directory);
                
                string[] passwordList = parsedLog.Authentications
                    .Where(x => !string.IsNullOrWhiteSpace(x.Password))
                    .Select(x => x.Password)
                    .Distinct()
                    .ToArray();

                if (SettingsDto?.ParserSettings.SaveAs != AuthSaveAs.SaveInDatabase)
                {
                    parsedLog.Authentications.Clear();
                    if (SettingsDto?.ParserSettings.SaveAs == AuthSaveAs.SaveInFile)
                    {
                        await File.AppendAllLinesAsync("Passwords.txt", passwordList);
                    }
                }

                foreach (var baseWallet in baseWallets)
                {
                    var walletType = baseWallet.walletType;
                    var walletDirectory = baseWallet.Directory;
                    var walletEncrypted = await baseWallet.GetEncrypted(SettingsDto.DecryptorSettings);
                    var walletHashcat = await baseWallet.GetHashcat(walletEncrypted);
                    
                    var searchedAddresses = await baseWallet.GetOriginalAddresses();
                    var grabbedAddresses = searchedAddresses.Select(x => new AddressInfoDto()
                    {
                        Address = x
                    });
                    
                    var decryptResult = SettingsDto.DecryptorSettings.TryDecrypt
                        ? await baseWallet.Decrypt(walletEncrypted, passwordList)
                        : null;
                    var decrypted = decryptResult?.Decrypted;
                    var password = decryptResult?.Password;
                    var secret = decryptResult?.Mnemonic;
                    var generatedAddresses = 
                        await _addressGeneratorServerMethod.GetEthereumAddressesInfoDto(secret, SettingsDto.DecryptorSettings.DepthGenerate)
                        ?? Array.Empty<AddressInfoDto>();
                    var allAddresses = grabbedAddresses
                        .Union(generatedAddresses)
                        .DistinctBy(x => x.Address, StringComparer.OrdinalIgnoreCase)
                        .ToArray();

                    var wallet = new WalletDto()
                    {
                        Directory = walletDirectory,
                        Type = walletType,
                        Hashcat = walletHashcat,
                        Decrypted = decrypted,
                        Password = password,
                        Secret = secret,
                        Encrypted = walletEncrypted,
                        HasBeenDecrypted = !string.IsNullOrWhiteSpace(secret),
                        Addresses = allAddresses
                    };
                    await _balanceServerMethodOverride.CheckAllAddresses(wallet.Addresses, SettingsDto.BalanceCheckerSettings);

                    var isSaveDecrypted = SettingsDto.DecryptorSettings.DecryptSaveAs == DecryptSaveAs.SaveAlways ||
                                          (SettingsDto.DecryptorSettings.DecryptSaveAs == DecryptSaveAs.SaveNotEmpty &&
                                           !string.IsNullOrWhiteSpace(wallet.Decrypted));
                    if (isSaveDecrypted)
                    {
                        parsedLog.Wallets?.Add(wallet);
                    }
                }
                
                dataMemory.Add(parsedLog);

                if (WorkHistoryDto.Current % 1000 == 0)
                {
                    var command = new CreateListResultCommand()
                    {
                        LogDtos = dataMemory
                    };
                    await Mediator.Send(command);
                    dataMemory.Clear();
                    
                    UpdateWorkHistoryCommand = new UpdateWorkHistoryCommand()
                    {
                        Id = WorkHistoryDto.Id,
                        Current = WorkHistoryDto.Current,
                        Total = WorkHistoryDto.Total,
                        Status = Status.AtWork
                    };
                    await Mediator.Send(UpdateWorkHistoryCommand);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
        
        if (dataMemory.Count > 0)
        {
            var command = new CreateListResultCommand()
            {
                LogDtos = dataMemory
            };
            await Mediator.Send(command);
            dataMemory.Clear();
        }
    }
    private async Task FillDataAsync(string path)
    {
        Directorys = Directory.GetDirectories(path).ToArray();
        SettingsDto = await Mediator.Send(new GetActiveSettingsQuery());
    }
}