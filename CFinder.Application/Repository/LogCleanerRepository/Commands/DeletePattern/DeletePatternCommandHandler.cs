using CFinder.Application.Common.Exceptions;
using CFinder.Application.Interfaces;
using CFinder.Domain.LogsCleaner;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CFinder.Application.Repository.LogCleanerRepository.Commands.DeletePattern;

internal class DeletePatternCommandHandler : IRequestHandler<DeletePatternCommand>
{
    private readonly IDataStore _dataStore;

    public DeletePatternCommandHandler(IDataStore dataStore)
    {
        _dataStore = dataStore;
    }
    
    public async Task Handle(DeletePatternCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dataStore.CleanerPatterns.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(CleanerPattern), request.Id);
        }

        _dataStore.CleanerPatterns.Remove(entity);
        await _dataStore.SaveChangesAsync(cancellationToken);
    }
}