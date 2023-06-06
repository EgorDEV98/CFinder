using CFinder.Application.Common.Exceptions;
using CFinder.Application.Common.Mappings;
using CFinder.Application.Interfaces;
using CFinder.Application.Models.Settings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CFinder.Application.Repository.SettingsRepository.Queries.GetSettingsById;

/// <summary>
/// Хендлер для получения профиля по ID
/// </summary>
internal class GetSettingsByIdQueryHandler : IRequestHandler<GetSettingsByIdQuery, SettingsDto>
{
    private readonly IDataStore _dataStore;

    public GetSettingsByIdQueryHandler(IDataStore dataStore)
    {
        _dataStore = dataStore;
    }
    
    public async Task<SettingsDto> Handle(GetSettingsByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dataStore.Settings
            .Include(x => x.DecryptorSettings)
            .Include(x => x.ParserSettings)
            .Include(x => x.BalanceCheckerSettings)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null || entity.Id != request.Id)
        {
            throw new NotFoundException(nameof(Domain.Settings), request.Id);
        }

        return entity.ToDto();
    }
}