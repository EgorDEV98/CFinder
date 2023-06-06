﻿using CFinder.Domain.Settings;

namespace CFinder.Application.Models.Settings;

public class DecryptorSettingsDto
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
    /// Глубина генерации
    /// </summary>
    public byte DepthGenerate { get; set; }
    
    /// <summary>
    /// Режим сохранения результатов
    /// </summary>
    public DecryptSaveAs DecryptSaveAs { get; set; }
    
    /// <summary>
    /// Режим парсинга кошельков
    /// </summary>
    public EncryptedParsingType EncryptedParsingType { get; set; }
    
    /// <summary>
    /// Кол-во иттераций в поиске циклом
    /// </summary>
    public int CycleItterationCount { get; set; }
    
}