using UnityEngine;

/// <summary>
/// Ion Barrel — Энергетический ствол с ионным фокусом.
/// Сверхточный ствол, почти без разброса.
/// </summary>
public class IonBarrel : Module, IBarrel
{
    [Header("Barrel Properties - Физика выстрела")]
    [SerializeField] private float _spread = 0.5f;          // Минимальный разброс
    [SerializeField] private int _projectileCount = 1;
    [SerializeField] private int _ricochetCount = 0;

    public float Spread => _spread;
    public int ProjectileCount => _projectileCount;
    public int RicochetCount => _ricochetCount;
}

