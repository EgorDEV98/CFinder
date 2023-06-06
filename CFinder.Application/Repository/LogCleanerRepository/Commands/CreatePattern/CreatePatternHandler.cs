using CFinder.Application.Interfaces;
using CFinder.Domain.LogsCleaner;
using MediatR;

namespace CFinder.Application.Repository.LogCleanerRepository.Commands.CreatePattern;

internal class CreatePatternHandler : IRequestHandler<CreatePatternCommand>
{
    private readonly IDataStore _dataStore;

    public CreatePatternHandler(IDataStore dataStore)
    {
        _dataStore = dataStore;
    }
    
    public async Task Handle(CreatePatternCommand request, CancellationToken cancellationToken)
    {
        var entity = new CleanerPattern()
        {
            Format = request.Format
        };

        await _dataStore.CleanerPatterns.AddAsync(entity, cancellationToken);
        await _dataStore.SaveChangesAsync(cancellationToken);
    }
}