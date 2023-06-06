using CFinder.Application.Common.Mappings;
using CFinder.Application.Interfaces;
using CFinder.Application.Models.CleanerPatterns.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CFinder.Application.Repository.LogCleanerRepository.Queries.GetPatternList;

internal class GetCleanerPatternsQueryHandler : IRequestHandler<GetCleanerPatternsQuery, CleanerPatternListDto>
{
    private readonly IDataStore _dataStore;

    public GetCleanerPatternsQueryHandler(IDataStore dataStore)
    {
        _dataStore = dataStore;
    }
    
    public async Task<CleanerPatternListDto> Handle(GetCleanerPatternsQuery request, CancellationToken cancellationToken)
    {
        var cleanerPatterns = await _dataStore.CleanerPatterns
            .Select(x => x.ToDto())
            .ToListAsync(cancellationToken);

        return new CleanerPatternListDto()
        {
            CleanerPatterns = cleanerPatterns
        };
    }
}