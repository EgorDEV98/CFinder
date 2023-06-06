using MediatR;

namespace CFinder.Application.Repository.LogCleanerRepository.Commands.CreatePattern;

public class CreatePatternCommand : IRequest
{
    public string Format { get; set; }
}