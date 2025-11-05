using UnityEngine;

/// <summary>
/// Heavy Rounds — Утяжелённые боеприпасы для увеличения мощности попадания.
/// +25% урон; снижена скорость полёта.
/// </summary>
public class HeavyRounds : Module, IAmmo
{
    [Header("Ammo Capacity")]
    [SerializeField] private int _amountMagazines = 1;
    [SerializeField] private int _maxAmmo = 15;
    [SerializeField] private int _amountAmmo = 8;
    
    [Header("Ammo Properties - Характеристики снаряда")]
    [SerializeField] private float _baseDamage = 12.5f;        // +25% урон
    [SerializeField] private float _projectileSpeed = 40f;     // Снижена скорость
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
    public float ProjectileRange => _projectileRange;// * RarityMultiplier;
}
