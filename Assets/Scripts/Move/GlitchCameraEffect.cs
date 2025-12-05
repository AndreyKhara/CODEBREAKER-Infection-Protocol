using UnityEngine;
using System;

namespace CDB.Input
{
    /// <summary>
    /// Компонент для применения глитч-эффектов к камере игрока.
    /// Работает совместно с PlayerCameraController - предоставляет offset который тот применяет.
    /// При окончании эффекта offset "вливается" в реальное положение камеры.
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
        
        // Текущий применяемый offset
        private Vector2 _currentOffset = Vector2.zero;
        
        // Отслеживание состояния эффектов для snap
        private bool _wasEffectActive = false;

        /// <summary>
        /// Событие вызывается когда эффект заканчивается.
        /// Передаёт offset который нужно применить к камере.
        /// </summary>
        public event Action<Vector2> OnEffectEnded;

        /// <summary>
        /// Активен ли какой-либо эффект
        /// </summary>
        public bool IsAnyEffectActive => _driftDirection != Vector2.zero || _jitterIntensity > 0f;

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
            bool isActive = IsAnyEffectActive;
            
            // Проверяем переход из активного состояния в неактивное
            if (_wasEffectActive && !isActive)
            {
                // Эффект только что закончился - делаем snap
                // Сообщаем PlayerCameraController что нужно применить offset к _cameraRotation
                OnEffectEnded?.Invoke(_currentOffset);
                
                // Обнуляем offset после snap
                _currentOffset = Vector2.zero;
                _accumulatedDrift = Vector2.zero;
            }
            
            _wasEffectActive = isActive;

            // Вычисляем целевой offset от активных эффектов
            if (_driftDirection != Vector2.zero)
            {
                _accumulatedDrift += _driftDirection * _driftSpeed * Time.deltaTime;
                
                // Ограничиваем максимальный дрейф
                _accumulatedDrift.x = Mathf.Clamp(_accumulatedDrift.x, -_maxDriftAngle, _maxDriftAngle);
                _accumulatedDrift.y = Mathf.Clamp(_accumulatedDrift.y, -_maxDriftAngle, _maxDriftAngle);
                
                _currentOffset = _accumulatedDrift;
            }

            // Добавляем jitter (временный, не накапливается)
            if (_jitterIntensity > 0f)
            {
                float jitterX = (Mathf.PerlinNoise(Time.time * _jitterFrequency, 0f) - 0.5f) * 2f * _jitterIntensity;
                float jitterY = (Mathf.PerlinNoise(0f, Time.time * _jitterFrequency) - 0.5f) * 2f * _jitterIntensity;
                
                // Jitter добавляется поверх drift, но не накапливается для snap
                // (jitter не должен влиять на финальную позицию после snap)
            }
        }

        /// <summary>
        /// Получить текущий offset для камеры (вызывается из PlayerCameraController).
        /// X = Yaw (горизонталь), Y = Pitch (вертикаль)
        /// </summary>
        public Vector2 GetCurrentOffset()
        {
            Vector2 offset = _currentOffset;
            
            // Добавляем jitter поверх (он временный и не участвует в snap)
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
            _currentOffset = Vector2.zero;
            _wasEffectActive = false;
        }
    }
}
