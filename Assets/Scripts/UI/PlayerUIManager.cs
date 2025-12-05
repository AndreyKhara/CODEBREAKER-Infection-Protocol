using UnityEngine;
using CDB.Character;

namespace CDB.UI
{
    public class PlayerUIManager : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private PlayerHealthBar _healthBar;

        [SerializeField] private GlitchManager _glitchEffect;

        private void Start()
        {
            if (_healthBar != null && _player != null)
            {
                _healthBar.UpdateHealth(_player.CurrentHealth, _player.MaxHealth);
            }
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

        private void OnPlayerHealthChanged(float currentHealth, float maxHealth)
        {
            if (_healthBar != null)
            {
                _healthBar.UpdateHealth(currentHealth, maxHealth);
            }
        }
    }
}
