using UnityEngine;

/// <summary>
/// AMMO - отвечает ТОЛЬКО за характеристики снаряда
/// </summary>
public interface IAmmo
{
    int AmountMagazines { get; set; }
    int MaxAmmo { get; set; }
    int AmountAmmo { get; set; }

    float BaseDamage { get; }           // Базовый урон снаряда
    float ProjectileSpeed { get; }      // Скорость полета снаряда
    float ProjectileRange { get; }      // Дальность полета (timeLife)
}
