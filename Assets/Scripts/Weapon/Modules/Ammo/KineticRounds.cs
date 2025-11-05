using UnityEngine;


public class KineticRounds : Module, IAmmo
{
    [Header("Ammo Capacity")]
    [SerializeField] private int _amountMagazines = 1;
    [SerializeField] private int _maxAmmo = 20;
    [SerializeField] private int _amountAmmo = 10;
    
    [Header("Ammo Properties - Характеристики снаряда")]
    [SerializeField] private float _baseDamage = 10f;
    [SerializeField] private float _projectileSpeed = 50f;
    [SerializeField] private float _projectileRange = 5f;   // TimeLife пули

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

    public float BaseDamage => _baseDamage; //* RarityMultiplier;
    public float ProjectileSpeed => _projectileSpeed;// * RarityMultiplier;
    public float ProjectileRange => _projectileRange;// * RarityMultiplier;*/
}
