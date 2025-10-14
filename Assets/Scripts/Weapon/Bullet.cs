using UnityEngine;
using CDB.Character;
public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f; // Время жизни пули в секундах
    public float damage = 10;

    [SerializeField] private Rigidbody _rb;
    private float startTime;

    private void Awake()
    {
        startTime = Time.time;

        // Задаем начальную скорость, двигаем вперед
        _rb.linearVelocity = transform.forward * speed;
    }


    private void Update()
    {
        // Автоматическое уничтожение пули по истечении времени жизни
        if (Time.time - startTime > lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {   
        IHealth health = collision.gameObject.GetComponent<IHealth>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
