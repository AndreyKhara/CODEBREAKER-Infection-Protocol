using UnityEngine;

public interface IAmmo
{
    int AmountMagazines { get; set; }
    int MaxAmmo { get; set; }
    int AmountAmmo { get; set; }

    float Speed { get; set; }
    float Damage{ get; set; }
}
