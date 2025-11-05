using UnityEngine;

/// <summary>
/// Pulse Rounds — Лёгкие боеприпасы с ускоренным импульсом.
/// Скорость полёта +20%; -15% урон.
/// </summary>
public class PulseRounds : Module, IAmmo
{
    [Header("Ammo Capacity")]
    [SerializeField] private int _amountMagazines = 1;
    [SerializeField] private int _maxAmmo = 25;
    [SerializeField] private int _amountAmmo = 15;
    
    [Header("Ammo Properties - Характеристики снаряда")]
    [SerializeField] private float _baseDamage = 8.5f;          // -15% урон
    [SerializeField] private float _projectileSpeed = 60f;      // +20% скорость
    [SerializeField] private float _projectileRange = 5f;

    public int AmountMagazines
    {
        get => _amountMagazines;
        set => _amountMagazines = value;
    }

    public int AmountAmmo
    {
        get => _amountAmmo;
        set => _amountAmmo = value;
    }

    public int MaxAmmo
    {
        get => _maxAmmo;
        set => _maxAmmo = value;
    }

    public float BaseDamage => _baseDamage;// * RarityMultiplier;
    public float ProjectileSpeed => _projectileSpeed;// * RarityMultiplier;
    public float ProjectileRange => _projectileRange;// * RarityMultiplier;*/
}
