using UnityEngine;

namespace CDB.Character.Enemy
{
    public class DummyTarget : EnemyBase
    {

        [SerializeField] private float _respawnDelay = 2f;
  
        public void OnCollisionEnter(Collision collision)
        {
            Debug.Log($"DummyTarget: OnCollisionEnter с {collision.gameObject.name}");
        }

        public override void TakeDamage(float damageAmount)
        {
            CurrentHealth -= damageAmount;
            Debug.Log($"DummyTarget получил урон: {damageAmount}, осталось здоровья: {CurrentHealth}");
            ShowDamage($"{damageAmount}");
            if (CurrentHealth <= 0)
            {
                Death();
            }
        }
        public override void Death()
        {
            Debug.Log("DummyTarget уничтожен! Восстановление через " + _respawnDelay + " сек.");
            _targetRenderer.enabled = false;
            Invoke(nameof(Respawn), _respawnDelay);
        }
        private void Respawn()
        {
            CurrentHealth = MaxHealth;
            _targetRenderer.enabled = true;
            ResetColor();
            Debug.Log("DummyTarget восстановлен!");
        }
    }
}
