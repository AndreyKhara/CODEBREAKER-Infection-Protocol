using UnityEngine;

namespace CDB.Input
{
    /// <summary>
    /// Компонент для применения глитч-эффектов к камере игрока.
    /// Работает совместно с PlayerCameraController - предоставляет offset который тот применяет.
    /// </summary>
    public class GlitchCameraEffect : MonoBehaviour
    {
        public static GlitchCameraEffect Instance { get; private set; }

        [Header("Drift Settings")]
        [SerializeField] private float _driftSpeed = 5f;
        [SerializeField] private float _maxDriftAngle = 20f;

        [Header("Jitter Settings")]
        [SerializeField] private float _jitterFrequency = 10f;

        // Текущие активные эффекты
        private Vector2 _driftDirection = Vector2.zero;
        private float _jitterIntensity = 0f;

        // Накопленное смещение от drift
        private Vector2 _accumulatedDrift = Vector2.zero;

        /// <summary>
        /// Направление и скорость дрейфа камеры для MouseDriftGlitch.
        /// Vector2.zero = эффект выключен.
        /// </summary>
        public Vector2 DriftDirection
        {
            get => _driftDirection;
            set => _driftDirection = value;
        }

        /// <summary>
        /// Интенсивность дрожания камеры для AimJitterGlitch.
        /// 0 = эффект выключен.
        /// </summary>
        public float JitterIntensity
        {
            get => _jitterIntensity;
            set => _jitterIntensity = value;
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogWarning("Multiple GlitchCameraEffect instances detected!");
            }
        }

        private void Update()
        {
            // Обновляем накопленный drift
            if (_driftDirection != Vector2.zero)
            {
                _accumulatedDrift += _driftDirection * _driftSpeed * Time.deltaTime;
                
                // Ограничиваем максимальный дрейф
                _accumulatedDrift.x = Mathf.Clamp(_accumulatedDrift.x, -_maxDriftAngle, _maxDriftAngle);
                _accumulatedDrift.y = Mathf.Clamp(_accumulatedDrift.y, -_maxDriftAngle, _maxDriftAngle);
            }
            else
            {
                // Плавно сбрасываем накопленный drift когда эффект выключен
                _accumulatedDrift = Vector2.Lerp(_accumulatedDrift, Vector2.zero, Time.deltaTime * 5f);
            }
        }

        /// <summary>
        /// Получить текущий offset для камеры (вызывается из PlayerCameraController).
        /// X = Yaw (горизонталь), Y = Pitch (вертикаль)
        /// </summary>
        public Vector2 GetCurrentOffset()
        {
            Vector2 offset = _accumulatedDrift;

            // Добавляем jitter
            if (_jitterIntensity > 0f)
            {
                float jitterX = (Mathf.PerlinNoise(Time.time * _jitterFrequency, 0f) - 0.5f) * 2f * _jitterIntensity;
                float jitterY = (Mathf.PerlinNoise(0f, Time.time * _jitterFrequency) - 0.5f) * 2f * _jitterIntensity;
                offset.x += jitterX;
                offset.y += jitterY;
            }

            return offset;
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        /// <summary>
        /// Сбросить все эффекты
        /// </summary>
        public void ResetAllEffects()
        {
            _driftDirection = Vector2.zero;
            _jitterIntensity = 0f;
            _accumulatedDrift = Vector2.zero;
        }
    }
}
