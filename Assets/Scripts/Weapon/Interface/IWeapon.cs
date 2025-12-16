using UnityEngine;

public interface IWeapon
{
    float Spread {get; set;}
    void Shoot();
    void Recharge();
    void ChangeModule(Module newModule);
    AudioClip[] AudioShoot {get;}
}
