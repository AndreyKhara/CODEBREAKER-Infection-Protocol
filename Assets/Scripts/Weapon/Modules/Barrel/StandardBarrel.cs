using UnityEngine;

/// <summary>
/// Standard Barrel — обычные пули прямой траекторией. Базовая стабильность: +10% точности
/// </summary>
public class StandardBarrel : Module, IBarrel
{
    [SerializeField, Tooltip("Увеличение точности (например, 0.1 = +10%)")] 
    private float _accuracyBonus = 0.1f;
    [SerializeField] private float _spread = 0f; // Минимальный разброс
    [SerializeField] private int _projectileCount = 1;

    public float AccuracyBonus => _accuracyBonus;

    public float Spread
    {
        get => _spread;
        private set => _spread = value;
    }

    public int ProjectileCount
    {
        get => _projectileCount;
        private set => _projectileCount = value;
    }

    // Можно добавить методы для применения бонуса точности к оружию
}
