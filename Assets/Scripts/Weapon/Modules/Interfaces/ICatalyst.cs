using UnityEngine;

/// <summary>
/// CATALYST - отвечает ТОЛЬКО за эффекты и модификаторы боя
/// </summary>
public interface ICatalyst
{
    GameObject BulletPrefab { get; }
    
    // Модификаторы боевых характеристик
    float FireRateMultiplier { get; }       // Скорострельность
    float AccuracyBonus { get; }            // Бонус к точности (снижение spread)
    
    // AOE эффекты
    bool HasAOEEffect { get; }
    float AOERadius { get; }
    float AOEDamageMultiplier { get; }      // Множитель от базового урона для AOE
    
    // Proc эффекты (шанс срабатывания)
    bool HasProcEffect { get; }
    float ProcChance { get; }               // Шанс 0-1
    StatusEffect ProcStatusEffect { get; }
    float ProcStatusDuration { get; }
    float ProcStatusValue { get; }          // Урон/значение эффекта
    
    // Система нестабильности
    float InstabilityGain { get; }
    
    string EffectDescription { get; }
}
