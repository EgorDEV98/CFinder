using CFinder.Application.Models.Settings;
using CFinder.Domain.Settings;
using FluentValidation;

namespace CFinder.Application.Repository.SettingsRepository.Commands.UpdateSettings;

/// <summary>
/// Валидатор команды обновления профилей
/// </summary>
internal class UpdateSettingsCommandValidation : AbstractValidator<UpdateSettingsCommand>
{
    public UpdateSettingsCommandValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name cannot be empty!")
            .MaximumLength(60).WithMessage("Max name length 60 characters")
            .MinimumLength(1).WithMessage("Min name length 1 character");
        RuleFor(x => x.DecryptorSettings)
            .NotEmpty().WithMessage("Cannot assign empty Crypto Finder Settings")
            .NotNull().WithMessage("Cannot assign empty Crypto Finder Settings");
        RuleFor(x => x.ParserSettings)
            .SetValidator(new ParserSettingsValidator());
        RuleFor(x => x.DecryptorSettings)
            .SetValidator(new DecryptorSettingsValidator());
        RuleFor(x => x.BalanceCheckerSettings)
            .SetValidator(new BalanceCheckerValidator());
    }

    private sealed class ParserSettingsValidator : AbstractValidator<ParserSettingsDto>
    {
        public ParserSettingsValidator()
        {
            RuleFor(x => x.ParsingType)
                .NotNull().WithMessage("Parsing type can not be null");
            RuleFor(x => x.SaveAs)
                .NotNull().WithMessage("Save as type can not be null");
        }
    }
    private sealed class DecryptorSettingsValidator : AbstractValidator<DecryptorSettingsDto>
    {
        public DecryptorSettingsValidator()
        {
            RuleFor(x => (int)x.DepthGenerate)
                .NotNull().WithMessage("Depth can not be null")
                .LessThanOrEqualTo(15).WithMessage("Depth must be less than or equal to 15")
                .GreaterThanOrEqualTo(1).WithMessage("Depth must be greater than or equal to 1");
            RuleFor(x => x.TryDecrypt)
                .NotNull().WithMessage("Available option \"Try Decrypt\" true or false");
            RuleFor(x => (int)x.ThreadCount)
                .NotNull()
                .GreaterThan(1).WithMessage("Min Thread 1");
            RuleFor(x => x.DecryptSaveAs)
                .NotNull().WithMessage("Decrypt save type can not be null");
            RuleFor(x => x.WalletParsingType)
                .NotNull().WithMessage("Wallet parsing type can not be null");
        }
    }
    
    private sealed class BalanceCheckerValidator : AbstractValidator<BalanceCheckerSettingsDto>
    {
        public BalanceCheckerValidator()
        {
            RuleFor(x => x.CheckCrypto)
                .NotNull().WithMessage("Available option \"Is Check Token\" true or false");
            RuleFor(x => (int)x.DelayBeforeRequest)
                .GreaterThan(0).WithMessage("Minimum delay 1ms")
                .NotNull().WithMessage("Available option \"Is Check NFT\" true or false");
        }
    }
}