using CFinder.Domain.LogsCleaner;
using FluentValidation;

namespace CFinder.Application.Repository.LogCleanerRepository.Commands.DeletePattern;

internal class DeletePatternCommandValidator : AbstractValidator<CleanerPattern>
{
    public DeletePatternCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage("Id can not be null")
            .GreaterThan(0).WithMessage("Id can not be 0");
    }
}