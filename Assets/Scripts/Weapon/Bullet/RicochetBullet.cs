using UnityEngine;
using CDB.Character;

/// <summary>
/// Рикошетная пуля для RicochetMalfunctionGlitch.
/// Отскакивает от поверхностей в случайном направлении вместо уничтожения.
/// </summary>
public class RicochetBullet : MonoBehaviour, IProjectile
{
    [SerializeField] private float _speed = 50f;
    [SerializeField] private float _timeLife = 5f;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private Rigidbody _rb;

    [Header("Ricochet Settings")]
    [SerializeField] private int _maxBounces = 3;
    [SerializeField] private float _randomAngleDeviation = 30f; // Случайное отклонение при отскоке
    [SerializeField] private float _damageReductionPerBounce = 0.7f; // Множитель урона после каждого отскока
    [SerializeField] private LayerMask _ricochetLayers; // Слои от которых отскакивает

    private float _spawnTime;
    private int _currentBounces = 0;

    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }

    public float TimeLife
    {
        get => _timeLife;
        set => _timeLife = value;
    }

    public float Damage
    {
        get => _damage;
        set => _damage = value;
    }

    private void Awake()
    {
        _spawnTime = Time.time;
        _rb.linearVelocity = transform.forward * Speed;
    }

    private void Update()
    {
        if (Time.time - _spawnTime > TimeLife)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Проверяем попадание по врагу
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IHealth health = collision.gameObject.GetComponent<IHealth>();
            if (health != null)
            {
                health.TakeDamage(_damage);
            }
            Destroy(gameObject);
            return;
        }

        // Попытка рикошета
        if (_currentBounces < _maxBounces)
        {
            PerformRicochet(collision);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Для триггер-коллайдеров врагов
        if (other.CompareTag("Enemy"))
        {
            IHealth health = other.GetComponent<IHealth>();
            if (health != null)
            {
                health.TakeDamage(_damage);
            }
            Destroy(gameObject);
        }
    }

    private void PerformRicochet(Collision collision)
    {
        _currentBounces++;

        // Получаем нормаль поверхности
        Vector3 normal = collision.contacts[0].normal;
        Vector3 incomingDirection = _rb.linearVelocity.normalized;

        // Вычисляем направление отражения
        Vector3 reflectedDirection = Vector3.Reflect(incomingDirection, normal);

        // Добавляем случайное отклонение для "глитчевости"
        float randomYaw = Random.Range(-_randomAngleDeviation, _randomAngleDeviation);
        float randomPitch = Random.Range(-_randomAngleDeviation, _randomAngleDeviation);
        Quaternion randomRotation = Quaternion.Euler(randomPitch, randomYaw, 0f);
        reflectedDirection = randomRotation * reflectedDirection;

        // Применяем новое направление
        _rb.linearVelocity = reflectedDirection.normalized * _speed;
        transform.forward = reflectedDirection.normalized;

        // Уменьшаем урон после отскока
        _damage *= _damageReductionPerBounce;

        // Визуальный эффект (опционально - можно добавить партиклы)
        // Debug.Log($"Ricochet! Bounces: {_currentBounces}/{_maxBounces}, Damage: {_damage}");
    }
}
