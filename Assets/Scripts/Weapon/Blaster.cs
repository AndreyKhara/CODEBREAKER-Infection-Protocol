using UnityEngine;


public class Blaster : MonoBehaviour, IWeapon
{
    [SerializeField] private int _amountMagazines = 1;
    [SerializeField] private int _amountAmmo = 10;
    [SerializeField] private int _maxAmmo = 20;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _barrelTransform;

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

    public void Shoot()
    {
        if (AmountAmmo != 0)
        {
            Debug.Log("shoot");
            Instantiate(_bulletPrefab, _barrelTransform.position, _barrelTransform.rotation);
            AmountAmmo -= 1;
        }
        else
        {
            Debug.Log("no Ammo");
            Recharge();
        }

    }

    public void Recharge()
    {
        AmountAmmo = MaxAmmo;
        Debug.Log("Recharge");
    }


}
