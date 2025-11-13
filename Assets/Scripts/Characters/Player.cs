using UnityEngine;

namespace CDB.Character
{
    public class Player : Character, IHealth
    {
        [SerializeField] private float _maxHealth = 100f;
        private float _currentHealth;

        private void Awake()
        {

            CurrentHealth = MaxHealth;
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
            set => _currentHealth = Mathf.Clamp(value, 0f, MaxHealth);
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
