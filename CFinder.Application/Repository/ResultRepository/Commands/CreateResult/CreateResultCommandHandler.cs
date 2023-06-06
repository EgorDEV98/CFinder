using CFinder.Application.Common.Mappings;
using CFinder.Application.Interfaces;
using MediatR;

namespace CFinder.Application.Repository.ResultRepository.Commands.CreateResult;

internal class CreateResultCommandHandler : IRequestHandler<CreateResultCommand>
{
    private readonly IDataStore _dataStore;

    public CreateResultCommandHandler(IDataStore dataStore)
    {
        _dataStore = dataStore;
    }
    
    public async Task Handle(CreateResultCommand request, CancellationToken cancellationToken)
    {
        var log = request.Log.ToDomain();

        await _dataStore.Logs.AddAsync(log, cancellationToken);
        await _dataStore.SaveChangesAsync(cancellationToken);
    }
}