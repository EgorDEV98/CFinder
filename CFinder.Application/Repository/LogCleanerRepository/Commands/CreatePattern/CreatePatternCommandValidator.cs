using CFinder.Domain.LogsCleaner;
using FluentValidation;

namespace CFinder.Application.Repository.LogCleanerRepository.Commands.CreatePattern;

internal class CreatePatternCommandValidator : AbstractValidator<CleanerPattern>
{
    public CreatePatternCommandValidator()
    {
        RuleFor(x => x.Format)
            .NotEmpty().WithMessage("A format cannot be empty")
            .Must(x => x.StartsWith('.')).WithMessage("The format must begin with . (dot). Example: .exe, .bin");
    }
}