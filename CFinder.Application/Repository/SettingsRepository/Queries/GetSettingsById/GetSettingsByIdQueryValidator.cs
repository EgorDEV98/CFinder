using FluentValidation;

namespace CFinder.Application.Repository.SettingsRepository.Queries.GetSettingsById;

/// <summary>
/// Валидатор для свойств/полей для получения профилей по ID
/// </summary>
internal class GetSettingsByIdQueryValidator : AbstractValidator<GetSettingsByIdQuery>
{
    public GetSettingsByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .GreaterThan(0).WithMessage("ID can not by 0");
    }
}