using UnityEngine;

/// <summary>
/// Cube Effect — Катализатор с большим AOE эффектом
/// </summary>
public class CubeEffect : Module, ICatalyst
{
    [Header("Catalyst Properties")]
    [SerializeField] private GameObject _prefabBullet;
    
    [Header("Модификаторы")]
    [SerializeField] private float _fireRateMultiplier = 0.9f;  // -10% скорострельность
    [SerializeField] private float _accuracyBonus = 0f;
    
    [Header("AOE Effect")]
    [SerializeField] private bool _hasAOEEffect = true;
    [SerializeField] private float _aoeRadius = 5f;
    [SerializeField] private float _aoeDamageMultiplier = 1f;   // 100% от базового урона
    
    [Header("Proc Effect")]
    [SerializeField] private bool _hasProcEffect = false;
    [SerializeField] private float _procChance = 0f;
    [SerializeField] private StatusEffect _procStatusEffect = StatusEffect.None;
    [SerializeField] private float _procStatusDuration = 0f;
    [SerializeField] private float _procStatusValue = 0f;
    
    [Header("Instability")]
    [SerializeField] private float _instabilityGain = 0.05f;    // +5% нестабильность

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
    public string EffectDescription => "AOE взрыв при попадании";
}
