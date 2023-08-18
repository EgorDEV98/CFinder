using CFinder.Application.Common.Exceptions;
using CFinder.Application.Interfaces;
using CFinder.Domain.Settings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CFinder.Application.Repository.SettingsRepository.Commands.DeleteSettingsCommand;

/// <summary>
/// Хендлер удаления профиля
/// </summary>
internal class DeleteSettingsCommandHandler : IRequestHandler<DeleteSettingsCommand>
{
    private readonly IDataStore _dataStore;

    public DeleteSettingsCommandHandler(IDataStore dataStore)
    {
        _dataStore = dataStore;
    }
    
    public async Task Handle(DeleteSettingsCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dataStore.Settings.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Settings), request.Id);
        }
        
        var anyEntity = await _dataStore.Settings
            .Where(x => x.Id != request.Id)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        if (anyEntity != null)
        {
            anyEntity.IsActive = true;
        }
        
        _dataStore.Settings.Remove(entity);
        
        await _dataStore.SaveChangesAsync(cancellationToken);
    }
}