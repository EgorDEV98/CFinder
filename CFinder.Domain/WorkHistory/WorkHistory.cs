using System.ComponentModel.DataAnnotations;

namespace CFinder.Domain.WorkHistory;

/// <summary>
/// Service history
/// </summary>
public class WorkHistory
{
    /// <summary>
    /// Identifier
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Operation name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Operation directory
    /// </summary>
    public string? Path { get; set; }
    
    /// <summary>
    /// Operation start date and time 
    /// </summary>
    public DateTime StartDate { get; set; }
    
    /// <summary>
    /// Operation end date and time 
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Total itteration
    /// </summary>
    public long Total { get; set; }
    
    /// <summary>
    /// Current itteration
    /// </summary>
    public long Current { get; set; }
    
    /// <summary>
    /// Work status
    /// </summary>
    public Status Status { get; set; }
}

/// <summary>
/// Work status
/// </summary>
public enum Status : byte
{
    [Display(Name = "Not started")]  NotStarted,
    [Display(Name = "At work")]      AtWork,
    [Display(Name = "Finished")]     Finished,
    [Display(Name = "Canceled")]     Canceled,
    [Display(Name = "Error")]        Error
}