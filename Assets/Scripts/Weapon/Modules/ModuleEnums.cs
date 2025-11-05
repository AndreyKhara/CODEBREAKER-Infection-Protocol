/// <summary>
/// Редкость модуля. Умножает бонусы модулей.
/// </summary>
public enum ModuleRarity
{
    Common = 0,     // 1.0x
    Uncommon = 1,   // 1.15x
    Rare = 2,       // 1.3x
    Epic = 3,       // 1.5x
    Legendary = 4,  // 1.75x
    Exotic = 5      // 2.0x
}

/// <summary>
/// Статус-эффекты от патронов
/// </summary>
public enum StatusEffect
{
    None,
    Burn,       // Урон со временем (огонь)
    Poison,     // Урон со временем (яд)
    Slow,       // Замедление
    Freeze,     // Заморозка
    Shock,      // Электрический разряд
    Corrosive   // Коррозия (снижение защиты)
}
