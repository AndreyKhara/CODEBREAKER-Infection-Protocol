using UnityEngine;

public class AOEBullet : Bullet
{
     //[SerializeField] private float aoeRadius = 1f;
    [SerializeField] private float aoeDamage = 10f; // Слабый урон, можно скорректировать
    [SerializeField] private GameObject impactEffectPrefab;
    protected override void OnHit()
    {
        if (impactEffectPrefab != null)
        {
            Instantiate(impactEffectPrefab, transform.position, Quaternion.identity);
        }
        
    }
}
