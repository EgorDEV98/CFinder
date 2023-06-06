using MediatR;

namespace CFinder.Application.Repository.LogCleanerRepository.Commands.DeletePattern;

public class DeletePatternCommand : IRequest
{
    public int Id { get; set; }
}