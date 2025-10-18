using System.Reflection;
using UnityEngine;

public class KineticRounds : Module, IAmmo
{
    [SerializeField] private int _amountMagazines = 1;
    [SerializeField] private int _maxAmmo = 20;
    [SerializeField] private int _amountAmmo = 10;
    [SerializeField] private float _speed = 50f;
    [SerializeField] private float _damage = 10f;
    

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

     public float Speed
    {
        get => _speed;
        set => _speed = value;
    }

    public float Damage
    {
        get => _damage;
        set => _damage = value;
    }

    
}
