using CFinder.Application.Models.CleanerPatterns.DTOs;
using CFinder.Application.Models.CleanerPatterns.VMs;
using CFinder.Application.Models.Settings;
using CFinder.Domain.LogsCleaner;
using CFinder.Domain.Settings;

namespace CFinder.Application.Mappings;

public static class CleanerPatternMapping
{
     /// <summary>
    /// DB Model to DTO
    /// </summary>
    /// <param name="settings"></param>
    /// <returns></returns>
    public static CleanerPatternDto ToDto(this CleanerPattern cleanerPattern)
     {
         return new CleanerPatternDto()
         {
             Id = cleanerPattern.Id,
             Format = cleanerPattern.Format
         };
     }

    /// <summary>
    /// DTO to DB Model
    /// </summary>
    /// <param name="settings"></param>
    /// <returns></returns>
    public static CleanerPattern ToDomain(this CleanerPatternDto cleanerPattern)
    {
        return new CleanerPattern()
        {
            Id = cleanerPattern.Id,
            Format = cleanerPattern.Format
        };
    }
    
    /// <summary>
    /// DTO to VM
    /// </summary>
    /// <param name="settings"></param>
    /// <returns></returns>
    public static CleanerPatternVm ToVm(this CleanerPatternDto cleanerPattern)
    {
        return new CleanerPatternVm()
        {
            Format = cleanerPattern.Format
        };
    }
}