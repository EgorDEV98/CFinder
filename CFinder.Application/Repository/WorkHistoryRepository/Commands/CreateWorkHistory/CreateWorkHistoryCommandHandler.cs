using CFinder.Application.Interfaces;
using CFinder.Application.Mappings;
using CFinder.Application.Models.WorkHistory.DTOs;
using CFinder.Domain.WorkHistory;
using MediatR;

namespace CFinder.Application.Repository.WorkHistoryRepository.Commands.CreateWorkHistory;

/// <summary>
/// Хендлер создания истории задачи
/// </summary>
internal class CreateWorkHistoryCommandHandler : IRequestHandler<CreateWorkHistoryCommand, WorkHistoryDto>
{
    private readonly IDataStore _dataStore;

    public CreateWorkHistoryCommandHandler(IDataStore dataStore)
    {
        _dataStore = dataStore;
    }
    
    public async Task<WorkHistoryDto> Handle(CreateWorkHistoryCommand request, CancellationToken cancellationToken)
    {
        var command = new WorkHistory()
        {
            Name = request.Name,
            Path = request.Path
        };

        await _dataStore.History.AddAsync(command, cancellationToken);
        await _dataStore.SaveChangesAsync(cancellationToken);

        return command.ToDto();
    }
}