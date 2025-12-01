using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using CDB.Input;

/// <summary>
/// Глитч дрожания прицела: камера случайно трясётся.
/// Затрудняет точное прицеливание.
/// </summary>
[Serializable]
public class AimJitterGlitch : GlitchEffect
{
    [SerializeField] private float _jitterIntensity = 3f;
    [SerializeField] private float _duration = 4f;

    public override void Apply(Gun gun)
    {
        var cameraEffect = GlitchCameraEffect.Instance;
        if (cameraEffect == null)
        {
            cameraEffect = UnityEngine.Object.FindFirstObjectByType<GlitchCameraEffect>();
        }

        if (cameraEffect == null)
        {
            Debug.LogWarning("AimJitterGlitch: GlitchCameraEffect не найден на сцене!");
            return;
        }

        ChangeValue(
            () => cameraEffect.JitterIntensity,
            (val) => cameraEffect.JitterIntensity = val,
            _jitterIntensity,
            _duration
        ).Forget();

        gun._instability = 30;
    }
}
