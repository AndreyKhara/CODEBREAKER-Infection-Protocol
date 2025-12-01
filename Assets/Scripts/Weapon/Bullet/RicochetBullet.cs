using UnityEngine;
using CDB.Character;

/// <summary>
/// Рикошетная пуля - отскакивает от стен и может нанести урон врагам и игроку.
/// </summary>
public class RicochetBullet : MonoBehaviour, IProjectile
{
    private const string TAG_ENEMY = "Enemy";
    private const string TAG_PLAYER = "Player";
    private const string TAG_WALL = "Wall";

    [Header("Bullet Settings")]
    [SerializeField] private float _speed = 50f;
    [SerializeField] private float _timeLife = 5f;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private Rigidbody _rb;

    [Header("Ricochet Settings")]
    [SerializeField] private int _maxBounces = 5;
    [SerializeField] private float _randomAngleDeviation = 8f;
    [SerializeField] private float _damageReductionPerBounce = 0.85f;
    [SerializeField] private LayerMask _ricochetLayers;

    [Header("Player Damage")]
    [SerializeField] private bool _canDamagePlayer = true;
    [SerializeField] private float _playerDamageMultiplier = 0.5f;

    private float _spawnTime;
    private int _currentBounces;

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
    }

    private void Start()
    {
        // Применяем скорость в Start, чтобы глитч-множители успели изменить Speed
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
        var target = collision.gameObject;

        if (TryDealDamage(target, TAG_ENEMY, _damage)) return;
        if (TryDealDamage(target, TAG_PLAYER, _damage * _playerDamageMultiplier, _canDamagePlayer)) return;

        TryRicochet(collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        var target = other.gameObject;

        if (TryDealDamage(target, TAG_ENEMY, _damage)) return;
        if (TryDealDamage(target, TAG_PLAYER, _damage * _playerDamageMultiplier, _canDamagePlayer)) return;

        if (target.CompareTag(TAG_WALL))
        {
            Destroy(gameObject);
        }
    }

    private bool TryDealDamage(GameObject target, string tag, float damage, bool condition = true)
    {
        if (!condition || !target.CompareTag(tag)) return false;

        if (target.TryGetComponent<IHealth>(out var health))
        {
            health.TakeDamage(damage);
        }
        Destroy(gameObject);
        return true;
    }

    private void TryRicochet(Collision collision)
    {
        if (_currentBounces < _maxBounces)
        {
            PerformRicochet(collision);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private bool IsWall(GameObject obj)
    {
        return obj.CompareTag(TAG_WALL) || (_ricochetLayers.value & (1 << obj.layer)) != 0;
    }

    private void PerformRicochet(Collision collision)
    {
        _currentBounces++;

        Vector3 normal = collision.contacts[0].normal;
        Vector3 reflected = Vector3.Reflect(_rb.linearVelocity.normalized, normal);

        // Небольшое случайное отклонение
        reflected = ApplyRandomDeviation(reflected);

        _rb.linearVelocity = reflected * _speed;
        transform.forward = reflected;
        _damage *= _damageReductionPerBounce;
    }

    private Vector3 ApplyRandomDeviation(Vector3 direction)
    {
        float yaw = Random.Range(-_randomAngleDeviation, _randomAngleDeviation);
        float pitch = Random.Range(-_randomAngleDeviation, _randomAngleDeviation);
        return (Quaternion.Euler(pitch, yaw, 0f) * direction).normalized;
    }
}
