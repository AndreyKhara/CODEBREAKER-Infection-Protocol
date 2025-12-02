using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

/// <summary>
/// Глитч замедления пуль: скорость ×0.2, урон ×2.
/// Пули летят медленно, но бьют сильнее.
/// </summary>
[Serializable]
public class SlowMotionBulletsGlitch : GlitchEffect
{
    [SerializeField] private float _speedMultiplier = 0.2f;
    [SerializeField] private float _damageMultiplier = 2f;
    [SerializeField] private float _duration = 5f;

    public override void Apply(Gun gun)
    {
        // Замедляем пули
        ChangeValue(
            () => gun.BulletSpeedMultiplier,
            (val) => gun.BulletSpeedMultiplier = val,
            _speedMultiplier,
            _duration
        ).Forget();

        // Увеличиваем урон
        ChangeValue(
            () => gun.DamageMultiplier,
            (val) => gun.DamageMultiplier = val,
            _damageMultiplier,
            _duration
        ).Forget();

        gun._instability = 30;
    }
}
