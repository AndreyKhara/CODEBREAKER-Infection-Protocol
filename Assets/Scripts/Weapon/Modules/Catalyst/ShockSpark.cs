using UnityEngine;

/// <summary>
/// Shock Spark — Генератор электростатического разряда.
/// 10% шанс оглушить врага на 1 секунду.
/// </summary>
public class ShockSpark : Module, ICatalyst
{
    [Header("Catalyst Properties")]
    [SerializeField] private GameObject _prefabBullet;
    
    [Header("Модификаторы")]
    [SerializeField] private float _fireRateMultiplier = 1f;
    [SerializeField] private float _accuracyBonus = 0f;
    
    [Header("AOE Effect")]
    [SerializeField] private bool _hasAOEEffect = false;
    [SerializeField] private float _aoeRadius = 0f;
    [SerializeField] private float _aoeDamageMultiplier = 0f;
    
    [Header("Proc Effect - Stun")]
    [SerializeField] private bool _hasProcEffect = true;
    [SerializeField] private float _procChance = 0.1f;          // 10% шанс
    [SerializeField] private StatusEffect _procStatusEffect = StatusEffect.Shock; // Оглушение
    [SerializeField] private float _procStatusDuration = 1f;    // 1 секунда
    [SerializeField] private float _procStatusValue = 0f;
    
    [Header("Instability")]
    [SerializeField] private float _instabilityGain = 0.02f;

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
    public float ProcStatusValue => _procStatusValue;
    
    public float InstabilityGain => _instabilityGain;
    public string EffectDescription => "10% шанс оглушить врага на 1 секунду.";
}
