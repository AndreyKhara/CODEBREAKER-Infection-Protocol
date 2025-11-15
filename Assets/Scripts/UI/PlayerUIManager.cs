using UnityEngine;
using CDB.Character;

namespace CDB.UI
{
    /// <summary>
    /// Управляет всем UI игрока, включая полоску здоровья
    /// </summary>
    public class PlayerUIManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Player _player;
        [SerializeField] private PlayerHealthBar _healthBar;

        [Header("Settings")]
        [SerializeField] private bool _autoFindPlayer = true;

        private void Awake()
        {
            // Автопоиск игрока, если не назначен
            if (_player == null && _autoFindPlayer)
            {
                _player = FindObjectOfType<Player>();
                
                if (_player == null)
                {
                    Debug.LogError("PlayerUIManager: Player не найден в сцене!");
                    return;
                }
            }

            ValidateReferences();
        }

        private void OnEnable()
        {
            if (_player != null)
            {
                _player.OnHealthChanged += OnPlayerHealthChanged;
            }
        }

        private void OnDisable()
        {
            if (_player != null)
            {
                _player.OnHealthChanged -= OnPlayerHealthChanged;
            }
        }

        private void Start()
        {
            // Инициализация healthBar с текущими значениями здоровья игрока
            if (_healthBar != null && _player != null)
            {
                _healthBar.SetHealthImmediate(_player.CurrentHealth, _player.MaxHealth);
            }
        }

        /// <summary>
        /// Обработчик изменения здоровья игрока
        /// </summary>
        private void OnPlayerHealthChanged(float currentHealth, float maxHealth)
        {
            if (_healthBar != null)
            {
                _healthBar.UpdateHealth(currentHealth, maxHealth);
            }
        }

        /// <summary>
        /// Проверка всех необходимых ссылок
        /// </summary>
        private void ValidateReferences()
        {
            if (_player == null)
            {
                Debug.LogError("PlayerUIManager: Player не назначен!");
            }

            if (_healthBar == null)
            {
                Debug.LogError("PlayerUIManager: PlayerHealthBar не назначен!");
            }
        }

        /// <summary>
        /// Установить ссылку на игрока вручную
        /// </summary>
        public void SetPlayer(Player player)
        {
            // Отписываемся от старого игрока
            if (_player != null)
            {
                _player.OnHealthChanged -= OnPlayerHealthChanged;
            }

            _player = player;

            // Подписываемся на нового игрока
            if (_player != null)
            {
                _player.OnHealthChanged += OnPlayerHealthChanged;
                
                if (_healthBar != null)
                {
                    _healthBar.SetHealthImmediate(_player.CurrentHealth, _player.MaxHealth);
                }
            }
        }
    }
}
