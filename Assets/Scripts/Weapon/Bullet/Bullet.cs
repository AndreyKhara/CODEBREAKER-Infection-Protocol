using UnityEngine;
using CDB.Character;


public class Bullet : MonoBehaviour, IProjectile
{
    [SerializeField] private float _speed = 50f;
    [SerializeField] private float _timeLife = 5f;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private Rigidbody _rb;
    
    [Header("Wall Collision")]
    [SerializeField] private LayerMask _wallLayers; // Слои стен для уничтожения пули
    
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
    }

    private void Start()
    {
        // Применяем скорость в Start, чтобы глитч-множители успели изменить Speed
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
            return;
        }

        // Уничтожение при попадании в стену
        if (other.CompareTag("Wall") || IsInLayerMask(other.gameObject.layer, _wallLayers))
        {
            Destroy(gameObject);
            return;
        }
    }

    /// <summary>
    /// Проверяет, находится ли слой объекта в указанной LayerMask
    /// </summary>
    private bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return layerMask.value != 0 && (layerMask.value & (1 << layer)) != 0;
    }

    protected virtual void ApplyEffect(Collider other)
    {
    }
}
