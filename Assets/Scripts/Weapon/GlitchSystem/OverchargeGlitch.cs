using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

/// <summary>
/// Глитч перезарядки: урон ×2, но разброс ×3.
/// Риск/награда - больше урона за меньшую точность.
/// </summary>
[Serializable]
public class OverchargeGlitch : GlitchEffect
{
    [SerializeField] private float _damageMultiplier = 2f;
    [SerializeField] private float _spreadMultiplier = 3f;
    [SerializeField] private float _duration = 5f;

    public override void Apply(Gun gun)
    {
        float originalSpread = gun.Spread;
        float boostedSpread = originalSpread * _spreadMultiplier;

        // Увеличиваем урон
        ChangeValue(
            () => gun.DamageMultiplier,
            (val) => gun.DamageMultiplier = val,
            _damageMultiplier,
            _duration
        ).Forget();

        // Увеличиваем разброс
        ChangeValue(
            () => gun.Spread,
            (val) => gun.Spread = val,
            boostedSpread,
            _duration
        ).Forget();

        gun._instability = 30;
    }
}
