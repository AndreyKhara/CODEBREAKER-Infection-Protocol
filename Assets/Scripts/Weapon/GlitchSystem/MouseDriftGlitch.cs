using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using CDB.Input;

/// <summary>
/// Глитч дрейфа мыши: прицел медленно "плывёт" в случайную сторону.
/// Требует постоянной коррекции от игрока.
/// </summary>
[Serializable]
public class MouseDriftGlitch : GlitchEffect
{
    [SerializeField] private float _driftStrength = 1.5f;
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
            Debug.LogWarning("MouseDriftGlitch: GlitchCameraEffect не найден на сцене!");
            return;
        }

        // Генерируем случайное направление дрейфа
        Vector2 randomDrift = new Vector2(
            UnityEngine.Random.Range(-1f, 1f),
            UnityEngine.Random.Range(-1f, 1f)
        ).normalized * _driftStrength;

        ChangeValue(
            () => cameraEffect.DriftDirection,
            (val) => cameraEffect.DriftDirection = val,
            randomDrift,
            _duration
        ).Forget();

        gun._instability = 30;
    }
}
