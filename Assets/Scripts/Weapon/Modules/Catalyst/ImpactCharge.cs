using UnityEngine;

/// <summary>
/// Impact Charge — Модуль, создающий кинетический импульс при попадании.
/// Каждое попадание наносит слабый АоЕ-урон в радиусе 1 м.
/// </summary>
public class ImpactCharge : Module, ICatalyst
{
    [Header("Catalyst Properties")]
    [SerializeField] private GameObject _prefabBullet;
    
    [Header("Модификаторы")]
    [SerializeField] private float _fireRateMultiplier = 1f;
    [SerializeField] private float _accuracyBonus = 0f;
    
    [Header("AOE Effect")]
    [SerializeField] private bool _hasAOEEffect = true;
    [SerializeField] private float _aoeRadius = 1f;
    [SerializeField] private float _aoeDamageMultiplier = 0.5f;  // 50% от базового урона
    
    [Header("Proc Effect")]
    [SerializeField] private bool _hasProcEffect = false;
    [SerializeField] private float _procChance = 0f;
    [SerializeField] private StatusEffect _procStatusEffect = StatusEffect.None;
    [SerializeField] private float _procStatusDuration = 0f;
    [SerializeField] private float _procStatusValue = 0f;
    
    [Header("Instability")]
    [SerializeField] private float _instabilityGain = 0f;

    public GameObject BulletPrefab => _prefabBullet;
    
    public float FireRateMultiplier => _fireRateMultiplier;
    public float AccuracyBonus => _accuracyBonus;
    
    public bool HasAOEEffect => _hasAOEEffect;
    public float AOERadius => _aoeRadius * RarityMultiplier;
    public float AOEDamageMultiplier => _aoeDamageMultiplier;
    
    public bool HasProcEffect => _hasProcEffect;
    public float ProcChance => _procChance;
    public StatusEffect ProcStatusEffect => _procStatusEffect;
    public float ProcStatusDuration => _procStatusDuration * RarityMultiplier;
    public float ProcStatusValue => _procStatusValue * RarityMultiplier;
    
    public float InstabilityGain => _instabilityGain;
    public string EffectDescription => "Каждое попадание наносит слабый АоЕ-урон в радиусе 1 м.";
}
