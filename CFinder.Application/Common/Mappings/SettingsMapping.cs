using CFinder.Application.Models.Settings;
using CFinder.Domain.Settings;

namespace CFinder.Application.Mappings;

public static class SettingsMapping
{
    /// <summary>
    /// DB Model to DTO
    /// </summary>
    /// <param name="settings"></param>
    /// <returns></returns>
    public static SettingsDto ToDto(this Settings settings)
    {
        return new SettingsDto()
        {
            Id = settings.Id,
            Name = settings.Name,
            IsActive = settings.IsActive,
            DecryptorSettings = new DecryptorSettingsDto()
            {
                Id = settings.DecryptorSettings.Id,
                DecryptSaveAs = settings.DecryptorSettings.DecryptSaveAs,
                DepthGenerate = settings.DecryptorSettings.DepthGenerate,
                EncryptedParsingType = settings.DecryptorSettings.EncryptedParsingType,
                TryDecrypt = settings.DecryptorSettings.TryDecrypt,
                CycleItterationCount = settings.DecryptorSettings.CycleItterationCount
            },
            BalanceCheckerSettings = new BalanceCheckerSettingsDto()
            {
                Id = settings.BalanceCheckerSettings.Id,
                CheckCrypto = settings.BalanceCheckerSettings.CheckCrypto,
                DelayBeforeRequest = settings.BalanceCheckerSettings.DelayBeforeRequest,
                OnlyWhiteList = settings.BalanceCheckerSettings.OnlyWhiteList
            },
            ParserSettings = new ParserSettingsDto()
            {
                Id = settings.ParserSettings.Id,
                ParsingType = settings.ParserSettings.ParsingType,
                SaveAs = settings.ParserSettings.SaveAs
            }
        };
    }

    /// <summary>
    /// DTO to DB Model
    /// </summary>
    /// <param name="settings"></param>
    /// <returns></returns>
    public static Settings ToDomain(this SettingsDto settings)
    {
        return new Settings()
        {
            Id = settings.Id,
            Name = settings.Name,
            IsActive = settings.IsActive,
            DecryptorSettings = new DecryptorSettings()
            {
                Id = settings.DecryptorSettings.Id,
                DecryptSaveAs = settings.DecryptorSettings.DecryptSaveAs,
                DepthGenerate = settings.DecryptorSettings.DepthGenerate,
                EncryptedParsingType = settings.DecryptorSettings.EncryptedParsingType,
                TryDecrypt = settings.DecryptorSettings.TryDecrypt,
                CycleItterationCount = settings.DecryptorSettings.CycleItterationCount
            },
            BalanceCheckerSettings = new BalanceCheckerSettings()
            {
                Id = settings.BalanceCheckerSettings.Id,
                CheckCrypto = settings.BalanceCheckerSettings.CheckCrypto,
                DelayBeforeRequest = settings.BalanceCheckerSettings.DelayBeforeRequest,
                OnlyWhiteList = settings.BalanceCheckerSettings.OnlyWhiteList
            },
            ParserSettings = new ParserSettings()
            {
                Id = settings.ParserSettings.Id,
                ParsingType = settings.ParserSettings.ParsingType,
                SaveAs = settings.ParserSettings.SaveAs
            }
        };
    }
    
    /// <summary>
    /// DTO to VM
    /// </summary>
    /// <param name="settings"></param>
    /// <returns></returns>
    public static SettingsVm ToVm(this SettingsDto settings)
    {
        return new SettingsVm()
        {
            Name = settings.Name,
            IsActive = settings.IsActive,
            DecryptorSettings = new DecryptorSettingsVm()
            {
                DecryptSaveAs = settings.DecryptorSettings.DecryptSaveAs,
                DepthGenerate = settings.DecryptorSettings.DepthGenerate,
                WalletParsingType = settings.DecryptorSettings.EncryptedParsingType,
                TryDecrypt = settings.DecryptorSettings.TryDecrypt,
                CycleItterationCount = settings.DecryptorSettings.CycleItterationCount
            },
            BalanceCheckerSettings = new BalanceCheckerSettingsVm()
            {
                CheckCrypto = settings.BalanceCheckerSettings.CheckCrypto,
                DelayBeforeRequest = settings.BalanceCheckerSettings.DelayBeforeRequest,
                OnlyWhiteList = settings.BalanceCheckerSettings.OnlyWhiteList
            },
            ParserSettings = new ParserSettingsVm()
            {
                ParsingType = settings.ParserSettings.ParsingType,
                SaveAs = settings.ParserSettings.SaveAs
            }
        };
    }
}