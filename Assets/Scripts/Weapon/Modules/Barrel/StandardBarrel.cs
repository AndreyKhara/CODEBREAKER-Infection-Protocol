using UnityEngine;

/// <summary>
/// Standard Barrel — Обычные пули прямой траекторией.
/// Минимальный разброс.
/// </summary>
public class StandardBarrel : Module, IBarrel
{
    [Header("Barrel Properties - Физика выстрела")]
    [SerializeField] private float _spread = 1f;            // Минимальный разброс
    [SerializeField] private int _projectileCount = 1;
    [SerializeField] private int _ricochetCount = 0;

    public float Spread => _spread;
    public int ProjectileCount => _projectileCount;
    public int RicochetCount => _ricochetCount;
}

