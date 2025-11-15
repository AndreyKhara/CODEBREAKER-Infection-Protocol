using UnityEngine;
using UnityEngine.UI;

namespace CDB.UI
{
    /// <summary>
    /// Управляет отображением полоски здоровья игрока
    /// </summary>
    public class PlayerHealthBar : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Image _healthBarFill;
        [SerializeField] private Image _healthBarBackground;
        
        [Header("Colors")]
        [SerializeField] private Color _highHealthColor = Color.green;
        [SerializeField] private Color _mediumHealthColor = Color.yellow;
        [SerializeField] private Color _lowHealthColor = Color.red;
        
        [Header("Thresholds")]
        [SerializeField] private float _mediumHealthThreshold = 0.5f;
        [SerializeField] private float _lowHealthThreshold = 0.25f;
        
        [Header("Animation")]
        [SerializeField] private float _smoothSpeed = 5f;

        [Header("Fill Mode")]
        [SerializeField] private FillMode _fillMode = FillMode.FilledImage;

        public enum FillMode
        {
            FilledImage,    // Использует Image.fillAmount (требует Image Type = Filled)
            Scale,          // Использует scale по оси X
            Width           // Использует RectTransform.sizeDelta
        }

        private float _targetFillAmount;
        private float _currentFillAmount;
        private RectTransform _fillRectTransform;
        private Vector2 _originalSize;
        private bool _isAnimating;
        private Color _currentColor;

        private void Awake()
        {
            ValidateComponents();
            
            if (_healthBarFill != null)
            {
                _fillRectTransform = _healthBarFill.rectTransform;
                if (_fillRectTransform != null)
                {
                    _originalSize = _fillRectTransform.sizeDelta;
                }
                _currentColor = _healthBarFill.color;
            }
        }

        private void Update()
        {
            if (!_isAnimating) return;

            // Плавное изменение полоски здоровья
            const float threshold = 0.001f;
            
            if (Mathf.Abs(_currentFillAmount - _targetFillAmount) < threshold)
            {
                _isAnimating = false;
                _currentFillAmount = _targetFillAmount;
                ApplyFillAmount(_targetFillAmount);
                return;
            }

            _currentFillAmount = Mathf.Lerp(_currentFillAmount, _targetFillAmount, Time.deltaTime * _smoothSpeed);
            ApplyFillAmount(_currentFillAmount);
        }

        private void ApplyFillAmount(float fillAmount)
        {
            switch (_fillMode)
            {
                case FillMode.FilledImage:
                    _healthBarFill.fillAmount = fillAmount;
                    break;
                    
                case FillMode.Scale:
                    if (_fillRectTransform != null)
                    {
                        var scale = _fillRectTransform.localScale;
                        scale.x = fillAmount;
                        _fillRectTransform.localScale = scale;
                    }
                    break;
                    
                case FillMode.Width:
                    if (_fillRectTransform != null)
                    {
                        var size = _fillRectTransform.sizeDelta;
                        size.x = _originalSize.x * fillAmount;
                        _fillRectTransform.sizeDelta = size;
                    }
                    break;
            }
        }

        /// <summary>
        /// Обновить полоску здоровья
        /// </summary>
        /// <param name="currentHealth">Текущее здоровье</param>
        /// <param name="maxHealth">Максимальное здоровье</param>
        public void UpdateHealth(float currentHealth, float maxHealth)
        {
            if (_healthBarFill == null || maxHealth <= 0)
                return;

            float healthPercent = Mathf.Clamp01(currentHealth / maxHealth);
            _targetFillAmount = healthPercent;
            _isAnimating = true;

            // Обновление цвета в зависимости от процента здоровья
            UpdateHealthColor(healthPercent);
        }

        /// <summary>
        /// Обновить цвет полоски здоровья в зависимости от процента
        /// </summary>
        private void UpdateHealthColor(float healthPercent)
        {
            if (_healthBarFill == null)
                return;

            Color targetColor = healthPercent > _mediumHealthThreshold ? _highHealthColor
                : healthPercent > _lowHealthThreshold ? _mediumHealthColor
                : _lowHealthColor;

            if (_currentColor != targetColor)
            {
                _currentColor = targetColor;
                _healthBarFill.color = targetColor;
            }
        }

        /// <summary>
        /// Установить здоровье мгновенно (без анимации)
        /// </summary>
        public void SetHealthImmediate(float currentHealth, float maxHealth)
        {
            if (_healthBarFill == null || maxHealth <= 0)
                return;

            float healthPercent = Mathf.Clamp01(currentHealth / maxHealth);
            _targetFillAmount = healthPercent;
            _currentFillAmount = healthPercent;
            _isAnimating = false;

            ApplyFillAmount(healthPercent);
            UpdateHealthColor(healthPercent);
        }

        /// <summary>
        /// Проверка наличия необходимых компонентов
        /// </summary>
        private void ValidateComponents()
        {
            if (_healthBarFill == null)
            {
                Debug.LogError("PlayerHealthBar: Health Bar Fill Image не назначен!");
            }

            if (_healthBarBackground == null)
            {
                Debug.LogWarning("PlayerHealthBar: Health Bar Background Image не назначен.");
            }
        }
    }
}
