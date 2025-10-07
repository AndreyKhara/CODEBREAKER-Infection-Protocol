using UnityEngine;

public interface IWeapon
{
    int AmountmMgazines { set; get; }
    int AmountAmmo { set; get; }

    void Shoot();
    void Recharge();
    
}
