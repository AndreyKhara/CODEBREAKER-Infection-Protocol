using UnityEngine;
using System;

namespace CDB.Character
{
    public class Player : Character, IHealth
    {
        [SerializeField] private float _maxHealth = 100f;
        private float _currentHealth;

        // Событие для уведомления UI об изменении здоровья
        public event Action<float, float> OnHealthChanged;

        private void Awake()
        {

            CurrentHealth = MaxHealth;
            // Инициализация UI
            OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
        }

        //health
        public float MaxHealth
        {
            get => _maxHealth;
            set
            {
                _maxHealth = Mathf.Max(0f, value);
                _currentHealth = Mathf.Min(_currentHealth, _maxHealth);
            }
        }

        public float CurrentHealth
        {
            get => _currentHealth;
            set
            {
                _currentHealth = Mathf.Clamp(value, 0f, MaxHealth);
                // Уведомление UI об изменении здоровья
                OnHealthChanged?.Invoke(_currentHealth, MaxHealth);
            }
        }

        public void TakeDamage(float damageAmount)
        {
            CurrentHealth -= damageAmount;
            if (CurrentHealth <= 0)
            {
                Death();
            }
            Debug.Log($"Hp player : {CurrentHealth}");
        }

        public void Heal(float healAmount)
        {
            CurrentHealth += healAmount;
            if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
        }

        public void Death()
        {
            Debug.Log("Умэрр");
        }
        // #health
        

    }
}
