using MediatR;

namespace CFinder.Application.Repository.LogCleanerRepository.Commands.CreatePattern;
#nullable disable
public class CreatePatternCommand : IRequest
{
    public string Format { get; set; }
}