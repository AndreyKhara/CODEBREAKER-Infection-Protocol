using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

/// <summary>
/// Глитч залипания курка: оружие автоматически стреляет без контроля игрока.
/// Тратит патроны и создаёт хаос.
/// </summary>
[Serializable]
public class TriggerLockGlitch : GlitchEffect
{
    [SerializeField] private float _duration = 3f;

    public override void Apply(Gun gun)
    {
        ChangeValue(
            () => gun.IsAutoFireLocked,
            (val) => gun.IsAutoFireLocked = val,
            true,
            _duration
        ).Forget();

        gun._instability = 30;
    }
}
