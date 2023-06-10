using CFinder.Application.Models.Result;
using MediatR;

namespace CFinder.Application.Repository.ResultRepository.Commands.CreateListResults;

public class CreateListResultCommand : IRequest
{
    public IList<LogDto> LogDtos { get; set; }
}