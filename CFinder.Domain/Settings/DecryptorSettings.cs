using System.ComponentModel.DataAnnotations;

namespace CFinder.Domain.Settings;

public class DecryptorSettings
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Декриптовать
    /// </summary>
    public bool TryDecrypt { get; set; }
    
    /// <summary>
    /// Кол-во потоков
    /// </summary>
    public byte ThreadCount { get; set; }
    
    /// <summary>
    /// Глубина генерации
    /// </summary>
    public byte DepthGenerate { get; set; }
    
    /// <summary>
    /// Режим сохранения результатов
    /// </summary>
    public DecryptSaveAs DecryptSaveAs { get; set; }
    
    /// <summary>
    /// Режим парсинга зашифрованного сообщения
    /// </summary>
    public EncryptedParsingType EncryptedParsingType { get; set; }
    
    /// <summary>
    /// Кол-во иттераций в поиске циклом
    /// </summary>
    public int CycleItterationCount { get; set; }

    /// <summary>
    /// Получить стандартные настройки
    /// </summary>
    public static DecryptorSettings GetDefault() 
        => new DecryptorSettings()
        {
            Id = 0,
            ThreadCount = 100,
            DepthGenerate = 3,
            TryDecrypt = true,
            DecryptSaveAs = DecryptSaveAs.SaveAlways,
            CycleItterationCount = 1000,
            EncryptedParsingType = EncryptedParsingType.Cycle
        };
}

/// <summary>
/// Режим сохранения результатов декриптовки
/// </summary>
public enum DecryptSaveAs : byte
{
    [Display(Name = "Save always")]    SaveAlways,
    [Display(Name = "Save not empty")] SaveNotEmpty
}

/// <summary>
/// Режим поиска кошельков
/// </summary>
public enum EncryptedParsingType : byte
{
    [Display(Name = "Regex mode")]     Regex,
    [Display(Name = "Cycle mode")]     Cycle
}