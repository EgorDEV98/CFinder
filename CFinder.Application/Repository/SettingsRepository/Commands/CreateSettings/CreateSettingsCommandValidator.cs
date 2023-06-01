using FluentValidation;

namespace CFinder.Application.Repository.SettingsRepository.Commands.CreateSettings;

/// <summary>
/// Валидатор полей создания профиля
/// </summary>
internal class CreateSettingsCommandValidator : AbstractValidator<CreateSettingsCommand>
{
    public CreateSettingsCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name cannot be empty")
            .NotNull().WithMessage("Name cannot be empty")
            .MaximumLength(60).WithMessage("Max name length 60 character")
            .MinimumLength(1).WithMessage("Min name length 1 character");
    }
}