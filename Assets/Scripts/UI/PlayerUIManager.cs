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
            if (_healthBar != null && _player != null)
            {
                _healthBar.UpdateHealth(_player.CurrentHealth, _player.MaxHealth);
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
    }
}
