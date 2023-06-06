using CFinder.Application.Models.CleanerPatterns.DTOs;
using CFinder.Application.Models.CleanerPatterns.VMs;
using CFinder.Domain.LogsCleaner;

namespace CFinder.Application.Common.Mappings;

public static class CleanerPatternMapping
{
     /// <summary>
    /// DB Model to DTO
    /// </summary>
    /// <param name="cleanerPattern">CleanerPattern obj</param>
    /// <returns></returns>
    public static CleanerPatternDto ToDto(this CleanerPattern cleanerPattern)
     {
         return new CleanerPatternDto()
         {
             Id     = cleanerPattern.Id,
             Format = cleanerPattern.Format
         };
     }

    /// <summary>
    /// DTO to DB Model
    /// </summary>
    /// <param name="cleanerPattern">CleanerPatternDto obj</param>
    /// <returns></returns>
    public static CleanerPattern ToDomain(this CleanerPatternDto cleanerPattern)
    {
        return new CleanerPattern()
        {
            Id     = cleanerPattern.Id,
            Format = cleanerPattern.Format
        };
    }
    
    /// <summary>
    /// DTO to VM
    /// </summary>
    /// <param name="cleanerPattern">CleanerPatternDto obj</param>
    /// <returns></returns>
    public static CleanerPatternVm ToVm(this CleanerPatternDto cleanerPattern)
    {
        return new CleanerPatternVm()
        {
            Format = cleanerPattern.Format
        };
    }
}