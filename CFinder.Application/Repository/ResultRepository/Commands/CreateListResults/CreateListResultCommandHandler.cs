using CFinder.Application.Common.Mappings;
using CFinder.Application.Interfaces;
using MediatR;

namespace CFinder.Application.Repository.ResultRepository.Commands.CreateListResults;

public class CreateListResultCommandHandler : IRequestHandler<CreateListResultCommand>
{
    private readonly IDataStore _dataStore;

    public CreateListResultCommandHandler(IDataStore dataStore)
    {
        _dataStore = dataStore;
    }
    
    public async Task Handle(CreateListResultCommand request, CancellationToken cancellationToken)
    {
        var entitys = request.LogDtos.Select(x => x.ToDomain());

        await _dataStore.Logs.AddRangeAsync(entitys, cancellationToken);
        await _dataStore.SaveChangesAsync(cancellationToken);
    }
}