﻿using CFinder.Domain.WorkHistory;

namespace CFinder.Application.Models.WorkHistory.VMs;

/// <summary>
/// Service history VM
/// </summary>
public class WorkHistoryVm
{
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
    /// Percentage of completion
    /// </summary>
    public double Percent => (double)Current / Total * 100;
    
    /// <summary>
    /// Work status
    /// </summary>
    public Status Status { get; set; }
}