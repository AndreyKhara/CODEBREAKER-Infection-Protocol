using UnityEngine;

public interface IWeapon
{
    void Shoot();
    void Recharge();
    void ChangeModule(Module newModule);

}
