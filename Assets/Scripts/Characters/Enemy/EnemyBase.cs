using UnityEngine;

namespace CDB.Character.Enemy
{
    public class EnemyBase : MonoBehaviour, IEnemy, IHealth
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _damage;
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
            }
        }

        public float CurrentHealth
        {
            get => _currentHealth;
            set => _currentHealth = Mathf.Clamp(value, 0f, MaxHealth);
        }

        public float Damage
        {
            get => _damage;
            set => _damage = value;
        }

        public float Speed
        {
            get => _moveSpeed;
            set => _moveSpeed = value;
        }

        public virtual void TakeDamage(float damageAmount)
        {
            CurrentHealth -= damageAmount;
            if (CurrentHealth <= 0)
            {
                Death();
            }
        }

        public virtual void Attack(IHealth health)
        {
            health.TakeDamage(Damage);
        }
        

        public virtual void Heal(float healAmount)
        {
            CurrentHealth += healAmount;
            if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
        }

        public virtual void Death()
        {
            Destroy(gameObject);
        }
        // #health

    }
}
