using UnityEngine;

public interface IWeapon
{
    int AmountMagazines { get; set; }
    int MaxAmmo { get; set; }
    int AmountAmmo { get; set; }

    void Shoot();
    void Recharge();
    
}
