using CFinder.Application.Models.Result;
using MediatR;

namespace CFinder.Application.Repository.ResultRepository.Commands.CreateResult;

public class CreateResultCommand : IRequest
{
    public LogDto Log { get; set; }
}