using System.ComponentModel.DataAnnotations;

namespace CFinder.Domain.Settings;

public class DecryptorSettings
{
    /// <summary>
    /// Identifier
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Try decrypt
    /// </summary>
    public bool TryDecrypt { get; set; }

    /// <summary>
    /// Depth generate (address path)
    /// </summary>
    public byte DepthGenerate { get; set; }
    
    /// <summary>
    /// Save result mode
    /// </summary>
    public DecryptSaveAs DecryptSaveAs { get; set; }
    
    /// <summary>
    /// Parsing encrypted mode
    /// </summary>
    public EncryptedParsingType EncryptedParsingType { get; set; }
    
    /// <summary>
    /// Max circle itteration
    /// </summary>
    public int CycleItterationCount { get; set; }

    /// <summary>
    /// Get default settings
    /// </summary>
    public static DecryptorSettings GetDefault() 
        => new DecryptorSettings()
        {
            Id = 0,
            DepthGenerate = 3,
            TryDecrypt = true,
            DecryptSaveAs = DecryptSaveAs.SaveAlways,
            CycleItterationCount = 1000,
            EncryptedParsingType = EncryptedParsingType.Cycle
        };
}

/// <summary>
/// Save result mode
/// </summary>
public enum DecryptSaveAs : byte
{
    [Display(Name = "Save always")]    SaveAlways,
    [Display(Name = "Save not empty")] SaveNotEmpty
}

/// <summary>
/// Search encrypt mode
/// </summary>
public enum EncryptedParsingType : byte
{
    [Display(Name = "Regex mode")]     Regex,
    [Display(Name = "Cycle mode")]     Cycle
}