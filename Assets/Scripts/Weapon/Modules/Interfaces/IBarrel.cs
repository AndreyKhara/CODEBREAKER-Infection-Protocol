using UnityEngine;

public interface IBarrel
{
    Transform GunPoint { get; }
    float Spread { get; }
    int ProjectileCount { get; }
    

}
