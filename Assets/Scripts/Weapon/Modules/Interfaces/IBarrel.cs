using UnityEngine;

/// <summary>
/// BARREL - отвечает ТОЛЬКО за физику выстрела
/// </summary>
public interface IBarrel
{
    float Spread { get; }               // Разброс выстрелов в градусах
    int ProjectileCount { get; }        // Количество снарядов за выстрел
    int RicochetCount { get; }          // Количество рикошетов
}

