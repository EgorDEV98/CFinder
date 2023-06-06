using CFinder.Domain.Settings;

namespace CFinder.Application.Models.Settings;

public class DecryptorSettingsDto
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
    public static DecryptorSettingsDto GetDefault() 
        => new()
        {
            Id = 0,
            DepthGenerate = 3,
            TryDecrypt = true,
            DecryptSaveAs = DecryptSaveAs.SaveAlways,
            CycleItterationCount = 1000,
            EncryptedParsingType = EncryptedParsingType.Cycle
        };
}
