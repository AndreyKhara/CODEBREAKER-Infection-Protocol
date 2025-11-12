using UnityEngine;
using CDB.Character;

public class AOEBullet : Bullet
{
    [SerializeField] private float _aoeRadius = 5f;
    [SerializeField] private GameObject _impactEffectPrefab;
    [SerializeField] private LayerMask _damageLayerMask = -1;

    protected override void ApplyEffect(Collider other)
    {
        // Создаем эффект взрыва
        if (_impactEffectPrefab != null)
        {
            Debug.Log("Explosion");
            Instantiate(_impactEffectPrefab, transform.position, Quaternion.identity);
        }

        // Наносим урон в радиусе взрыва
        ApplyAOEDamage();
    }

    private void ApplyAOEDamage()
    {
        // TO DO fix delete Layer 
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _aoeRadius, _damageLayerMask);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                IHealth health = hitCollider.GetComponent<IHealth>();
                if (health != null)
                {
                    health.TakeDamage(Damage);
                }
            }
        }
    }
}
