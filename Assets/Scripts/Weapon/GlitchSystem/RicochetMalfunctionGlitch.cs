using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

/// <summary>
/// Глитч рикошета: пули отскакивают от стен в случайном направлении.
/// Опасно - можно попасть в себя!
/// </summary>
[Serializable]
public class RicochetMalfunctionGlitch : GlitchEffect
{
    [SerializeField] private float _duration = 6f;

    public override void Apply(Gun gun)
    {
        if (gun.RicochetBulletPrefab == null)
        {
            Debug.LogWarning("RicochetMalfunctionGlitch: RicochetBulletPrefab не назначен в Gun!");
            return;
        }

        ChangeValue(
            () => gun.UseRicochetBullet,
            (val) => gun.UseRicochetBullet = val,
            true,
            _duration
        ).Forget();

        gun._instability = 30;
    }
}
