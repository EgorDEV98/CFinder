using FluentValidation;

namespace CFinder.Application.Repository.SettingsRepository.Commands.DeleteSettingsCommand;

/// <summary>
/// Валидатор полей для удаления профилей
/// </summary>
public class DeleteSettingsCommandValidator : AbstractValidator<DeleteSettingsCommand>
{
    public DeleteSettingsCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEqual(0).WithMessage("Id can not be 0")
            .NotNull().WithMessage("Id ca not be null");
    }
}