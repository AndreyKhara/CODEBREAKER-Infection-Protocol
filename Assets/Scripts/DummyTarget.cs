using UnityEngine;

namespace CDB.Character.Enemy
{
    public class DummyTarget : EnemyBase
    {
        // TO DO : _value
        [SerializeField] private float _respawnDelay = 2f;
        [SerializeField] private Renderer _targetRenderer;
        [SerializeField] private Color _damageColor = Color.red;
        [SerializeField] private Color _normalColor = Color.white;


        private void Start()
        {
            UpdateColor(_normalColor);
        }

        public void OnCollisionEnter(Collision collision)
        {
            Debug.Log($"DummyTarget: OnCollisionEnter с {collision.gameObject.name}");
        }

        public override void TakeDamage(float damageAmount)
        {
            CurrentHealth -= damageAmount;
            Debug.Log($"DummyTarget получил урон: {damageAmount}, осталось здоровья: {CurrentHealth}");
            ShowDamage();
            if (CurrentHealth <= 0)
            {
                Death();
            }
        }

        private void ShowDamage()
        {
            UpdateColor(_damageColor);
            CancelInvoke(nameof(ResetColor));
            Invoke(nameof(ResetColor), 0.2f);
        }

        private void ResetColor()
        {
            UpdateColor(_normalColor);
        }

        private void UpdateColor(Color color)
        {
            _targetRenderer.material.color = color;

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
            UpdateColor(_normalColor);
            Debug.Log("DummyTarget восстановлен!");
        }
    }
}
