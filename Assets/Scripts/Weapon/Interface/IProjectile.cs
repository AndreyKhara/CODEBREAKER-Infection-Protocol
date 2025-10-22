using UnityEngine;

public interface IProjectile
{
    float TimeLife { get; set; }
    float Speed { get; set; }
    float Damage { get; set; }

}
