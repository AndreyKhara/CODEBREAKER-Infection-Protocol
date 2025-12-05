using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

/// <summary>
/// Глитч рикошета: пули отскакивают от стен. Опасно - можно попасть в себя!
/// </summary>
[Serializable]
public class RicochetMalfunctionGlitch : GlitchEffect
{
    [SerializeField] private float _duration = 6f;

    public override void Apply(Gun gun)
    {
        if (gun.RicochetBulletPrefab == null)
        {
            Debug.LogWarning("RicochetMalfunctionGlitch: RicochetBulletPrefab не назначен!");
            return;
        }

        EnableRicochetAsync(gun).Forget();
        gun._instability = 30;
    }

    private async UniTaskVoid EnableRicochetAsync(Gun gun)
    {
        gun.UseRicochetBullet = true;
        await UniTask.Delay(TimeSpan.FromSeconds(_duration), ignoreTimeScale: false);
        gun.UseRicochetBullet = false;
    }
}
