using CFinder.Application.Common.Exceptions;
using CFinder.Application.Interfaces;
using CFinder.Application.Mappings;
using CFinder.Application.Models.Settings;
using CFinder.Domain.Settings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CFinder.Application.Repository.SettingsRepository.Commands.UpdateSettings;

/// <summary>
/// Хендлер команды обновления профиля
/// </summary>
internal class UpdateSettingsCommandHandler : IRequestHandler<UpdateSettingsCommand, SettingsDto>
{
    private readonly IDataStore _dataStore;

    public UpdateSettingsCommandHandler(IDataStore dataStore)
    {
        _dataStore = dataStore;
    }
    
    public async Task<SettingsDto> Handle(UpdateSettingsCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dataStore.Settings.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null || entity.Id != request.Id)
        {
            throw new NotFoundException(nameof(Domain.Settings), request.Id);
        }
        await _dataStore.Settings.ForEachAsync(x => x.IsActive = false, cancellationToken);
        
        entity.Name = request.Name;
        entity.IsActive = true;
        entity.DecryptorSettings = new DecryptorSettings()
        {
            Id = request.DecryptorSettings.Id,
            DepthGenerate = request.DecryptorSettings.DepthGenerate,
            TryDecrypt = request.DecryptorSettings.TryDecrypt,
            DecryptSaveAs = request.DecryptorSettings.DecryptSaveAs,
            EncryptedParsingType = request.DecryptorSettings.EncryptedParsingType,
            CycleItterationCount = request.DecryptorSettings.CycleItterationCount
        };
        entity.ParserSettings = new ParserSettings()
        {
            Id = request.ParserSettings.Id,
            ParsingType = request.ParserSettings.ParsingType,
            SaveAs = request.ParserSettings.SaveAs
        };
        entity.BalanceCheckerSettings = new BalanceCheckerSettings()
        {
            Id = request.BalanceCheckerSettings.Id,
            CheckCrypto = request.BalanceCheckerSettings.CheckCrypto,
            DelayBeforeRequest = request.BalanceCheckerSettings.DelayBeforeRequest,
            OnlyWhiteList = request.BalanceCheckerSettings.OnlyWhiteList
        };


        await _dataStore.SaveChangesAsync(cancellationToken);

        return entity.ToDto();
    }
}