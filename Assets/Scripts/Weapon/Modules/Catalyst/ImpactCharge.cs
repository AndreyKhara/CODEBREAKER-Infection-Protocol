using UnityEngine;

public class ImpactCharge : Module, ICatalyst
{
    [SerializeField] private float aoeRadius = 1f;
    [SerializeField] private float aoeDamage = 10f; // Слабый урон, можно скорректировать
    [SerializeField] private GameObject impactEffectPrefab;
    [SerializeField] private GameObject _prefabBullet;

    public void OnHit(Vector3 position)
    {
        // Воспроизвести эффект
        if (impactEffectPrefab != null)
        {
            GameObject.Instantiate(impactEffectPrefab, position, Quaternion.identity);
        }
        // Найти все объекты в радиусе и нанести урон
        Collider[] colliders = Physics.OverlapSphere(position, aoeRadius);
        foreach (var collider in colliders)
        {
            var health = collider.GetComponent<IHealth>();
            if (health != null)
            {
                health.TakeDamage(aoeDamage);
            }
        }
    }

    public GameObject BulletPrefab => _prefabBullet;
}