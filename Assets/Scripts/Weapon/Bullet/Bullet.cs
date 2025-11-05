using UnityEngine;
using CDB.Character;


public class Bullet : MonoBehaviour, IProjectile
{
    [SerializeField] private float _speed = 50f;
    [SerializeField] private float _timeLife = 5f;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private Rigidbody _rb;
    
    private float spawnTime;

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
        spawnTime = Time.time;
        // Задаем начальную скорость, двигаем вперед
        _rb.linearVelocity = transform.forward * Speed;
    }

    private void Update()
    {
        // Автоматическое уничтожение пули по истечении времени жизни
        if (Time.time - spawnTime > TimeLife)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OnBulletHit(other);
    }

    protected virtual void OnBulletHit(Collider other)
    {   
        if (other.CompareTag("Enemy"))
        {
            IHealth health = other.GetComponent<IHealth>();
            if (health != null)
            {
                health.TakeDamage(_damage);

            }
            // TO DO
            ApplyEffect(other);
            Destroy(gameObject);
        }

    }

    protected virtual void ApplyEffect(Collider other)
    {
    }
}
