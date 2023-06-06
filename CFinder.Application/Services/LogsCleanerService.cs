using System.Diagnostics;
using CFinder.Application.Models.WorkHistory.DTOs;
using CFinder.Application.Repository.LogCleanerRepository.Queries.GetPatternList;
using CFinder.Application.Repository.WorkHistoryRepository.Commands.CreateWorkHistory;
using CFinder.Application.Repository.WorkHistoryRepository.Commands.UpdateWorkHistory;
using CFinder.Domain.WorkHistory;

namespace CFinder.Application.Services;

public class LogsCleanerService : BaseService
{
    private const string SERVICE_NAME = "Logs Cleaner Service";
    private WorkHistoryDto? WorkHistoryDto { get; set; }
    private UpdateWorkHistoryCommand UpdateWorkHistoryCommand { get; set; }
    
    public LogsCleanerService(IServiceProvider serviceProvider) : 
        base(serviceProvider)
    {
        
    }

    public async Task StartAsync(string path)
    {
        if (!Directory.Exists(path))
        {
            return;
        }

        var directorys = Directory.GetDirectories(path, "*");
        
        var query = new GetCleanerPatternsQuery();
        var cleanerPatternListDto = await Mediator.Send(query);
        
        var patterns = cleanerPatternListDto?
            .CleanerPatternDtos?
            .Select(x => x.Format)
            .ToArray();
        if (patterns == null)
        {
            return;
        }


        try
        {
            var command = new CreateWorkHistoryCommand()
            {
                Name = SERVICE_NAME,
                Path = path,
                Total = directorys.Length
            };
            WorkHistoryDto = await Mediator.Send(command);
            WorkHistoryDto.Current = 0;
        
            foreach (var directory in directorys)
            {
                ++WorkHistoryDto.Current;
            
                var badFiles = Directory.GetFiles(directory, "*", SearchOption.AllDirectories)
                    .Where(file => patterns.Any(pattern => file.EndsWith(pattern, StringComparison.OrdinalIgnoreCase)))
                    .ToList();

                foreach (var badFile in badFiles)
                {
                    File.Delete(badFile);
                }

                if (WorkHistoryDto.Current % 1000 == 0)
                {
                    UpdateWorkHistoryCommand = new UpdateWorkHistoryCommand()
                    {
                        Id = WorkHistoryDto.Id,
                        Current = WorkHistoryDto.Current,
                        Total = WorkHistoryDto.Total,
                        Status = Status.AtWork
                    };
                    await Mediator.Send(UpdateWorkHistoryCommand);
                }
            }
            
            UpdateWorkHistoryCommand = new UpdateWorkHistoryCommand()
            {
                Id = WorkHistoryDto.Id,
                Current = WorkHistoryDto.Current,
                Total = WorkHistoryDto.Total,
                Status = Status.Finished,
                EndDate = DateTime.Now
            };
            await Mediator.Send(UpdateWorkHistoryCommand);
        }
        catch
        {
            var updateCommand = new UpdateWorkHistoryCommand()
            {
                Id = WorkHistoryDto.Id,
                Current = WorkHistoryDto.Current,
                Total = WorkHistoryDto.Total,
                Status = Status.Error,
                EndDate = DateTime.Now
            };
            await Mediator.Send(updateCommand);
        }
        
    }
}