using UnityEngine;

/// <summary>
/// Double Shot — Разделяет энергетический заряд на два слабых луча.
/// Выпускает 2 выстрела с разбросом +3°.
/// </summary>
public class DoubleShot : Module, IBarrel
{
    [Header("Barrel Properties - Физика выстрела")]
    [SerializeField] private float _spread = 3f;            // Разброс 3°
    [SerializeField] private int _projectileCount = 2;      // Выпускает 2 выстрела
    [SerializeField] private int _ricochetCount = 0;

    public float Spread => _spread;
    public int ProjectileCount => _projectileCount;
    public int RicochetCount => _ricochetCount;
}


