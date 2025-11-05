using UnityEngine;

/// <summary>
/// Focused Barrel — Ствол с усиленной стабилизацией, концентрирующий траекторию выстрела.
/// Снижает разброс почти до нуля.
/// </summary>
public class FocusedBarrel : Module, IBarrel
{
    [Header("Barrel Properties - Физика выстрела")]
    [SerializeField] private float _spread = 0.1f;          // Почти нулевой разброс
    [SerializeField] private int _projectileCount = 1;
    [SerializeField] private int _ricochetCount = 0;

    public float Spread => _spread;
    public int ProjectileCount => _projectileCount;
    public int RicochetCount => _ricochetCount;
}

