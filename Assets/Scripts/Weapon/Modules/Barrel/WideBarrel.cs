using UnityEngine;

/// <summary>
/// Wide Barrel — ствол с расширенным дульным срезом, увеличивающий охват огня.
/// Увеличивает угол разброса пуль; каждая пуля наносит -15% урона.
/// </summary>
public class WideBarrel : Module, IBarrel
{
    [SerializeField, Tooltip("Увеличенный угол разброса пуль")] 
    private float _spread = 7f; // Пример: больше стандартного
    [SerializeField, Tooltip("Модификатор урона для каждой пули (например, 0.85 = -15%)")]
    private float _damageMultiplier = 0.85f;
    [SerializeField] private int _projectileCount = 1;

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

    public float DamageMultiplier => _damageMultiplier;

    // Для применения модификатора урона потребуется поддержка в логике стрельбы
}
