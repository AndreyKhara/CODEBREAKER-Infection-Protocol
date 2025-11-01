using UnityEngine;

/// <summary>
/// Tracer Module — Боеприпасы с визуальными трассерами для удобства прицеливания.
/// Выстрелы оставляют яркий след; +10% точности.
/// 2.5% шанс нанести DoT урон 10 сек.
/// 2.5% шанс снять врагу 75% защиты на 5 сек.
/// </summary>
public class TracerModule : Module, ICatalyst
{
    [Header("Catalyst Properties")]
    [SerializeField] private GameObject _prefabBullet;
    
    [Header("Модификаторы")]
    [SerializeField] private float _fireRateMultiplier = 1f;
    [SerializeField] private float _accuracyBonus = 0.1f;       // +10% точности (снижает spread)
    
    [Header("AOE Effect")]
    [SerializeField] private bool _hasAOEEffect = false;
    [SerializeField] private float _aoeRadius = 0f;
    [SerializeField] private float _aoeDamageMultiplier = 0f;
    
    [Header("Proc Effect - Mixed")]
    [SerializeField] private bool _hasProcEffect = true;
    [SerializeField] private float _procChance = 0.05f;         // 5% общий шанс (2.5% DoT + 2.5% Corrosive)
    [SerializeField] private StatusEffect _procStatusEffect = StatusEffect.Burn; // По умолчанию DoT
    [SerializeField] private float _procStatusDuration = 10f;   // 10 секунд для DoT
    [SerializeField] private float _procStatusValue = 2f;       // Урон DoT per tick
    
    [Header("Instability")]
    [SerializeField] private float _instabilityGain = 0f;

    public GameObject BulletPrefab => _prefabBullet;
    
    public float FireRateMultiplier => _fireRateMultiplier;
    public float AccuracyBonus => _accuracyBonus;
    
    public bool HasAOEEffect => _hasAOEEffect;
    public float AOERadius => _aoeRadius;
    public float AOEDamageMultiplier => _aoeDamageMultiplier;
    
    public bool HasProcEffect => _hasProcEffect;
    public float ProcChance => _procChance;
    public StatusEffect ProcStatusEffect => _procStatusEffect;
    public float ProcStatusDuration => _procStatusDuration * RarityMultiplier;
    public float ProcStatusValue => _procStatusValue * RarityMultiplier;
    
    public float InstabilityGain => _instabilityGain;
    public string EffectDescription => "Выстрелы оставляют яркий след; +10% точности. 2.5% DoT, 2.5% -75% защиты.";
}
