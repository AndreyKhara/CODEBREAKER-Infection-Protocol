using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FGT.Prototypes.DamagePopup;

namespace CDB.Character.Enemy
{
    public class EnemyBase : MonoBehaviour, IEnemy, IHealth
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _damage;
        
        [SerializeField] protected Renderer _targetRenderer;
        [SerializeField] private Color _damageColor = Color.red;
        [SerializeField] private Color _normalColor = Color.white;
        [SerializeField] private Transform _enemyTransform;
        // TO DO: Adressables
        [SerializeField] private GameObject[] _modules;
        private AddRoom room;
        private float _currentHealth;

        private void Awake()
        {
            CurrentHealth = MaxHealth;
        }

        protected virtual void Start()
        {
            //TO DO: Zenject
            room = GetComponentInParent<AddRoom>();
        }


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
            ShowDamage($"{damageAmount}");
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
            Instantiate(_modules[UnityEngine.Random.Range(0, _modules.Length-1)], transform.position, transform.rotation);
            Destroy(gameObject);
        }

        protected void ShowDamage(string textForShow)
        {
            DamagePopup.Create(textForShow, 2.5f * Vector3.up, _enemyTransform, Color.white);
            UpdateColor(_damageColor);
            CancelInvoke(nameof(ResetColor));
            Invoke(nameof(ResetColor), 0.2f);
        }

        protected void ResetColor()
        {
            UpdateColor(_normalColor);
        }

        protected void UpdateColor(Color color)
        {
            _targetRenderer.material.color = color;

        }

        private void OnDestroy()
        {
            room?.enemies?.Remove(gameObject);
        }
    }
}
