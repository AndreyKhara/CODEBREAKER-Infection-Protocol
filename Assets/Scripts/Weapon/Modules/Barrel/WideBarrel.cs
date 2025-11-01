using UnityEngine;

/// <summary>
/// Wide Barrel — Ствол с расширенным дульным срезом, увеличивающий охват огня.
/// Увеличивает угол разброса пуль.
/// </summary>
public class WideBarrel : Module, IBarrel
{
    [Header("Barrel Properties - Физика выстрела")]
    [SerializeField] private float _spread = 7f;            // Широкий разброс
    [SerializeField] private int _projectileCount = 1;
    [SerializeField] private int _ricochetCount = 0;

    public float Spread => _spread;
    public int ProjectileCount => _projectileCount;
    public int RicochetCount => _ricochetCount;
}

