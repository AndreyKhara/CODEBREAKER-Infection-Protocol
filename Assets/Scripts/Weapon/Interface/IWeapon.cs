using UnityEngine;

public interface IWeapon
{
    //IAmmo AmmoModule { get; set; }
    //IBarrel BarrelModule { get; set; }

    void Shoot();
    void Recharge();
    void ChangeModule(Module newModule);

}
