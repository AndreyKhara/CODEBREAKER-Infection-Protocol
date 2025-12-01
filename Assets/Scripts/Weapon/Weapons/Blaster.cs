using UnityEngine;

public class Blaster : Gun
{
    public override void Shoot()
    {
        if (_stopShoot) return;
        
        if (_ammoModule.AmountAmmo < _barrelModule.ProjectileCount)
        {
            Recharge();
            return;
        }

        for (int i = 0; i < _barrelModule.ProjectileCount; i++)
        {
            SpawnBullet();
            AddInstability();
        }
        
        _ammoModule.AmountAmmo -= _barrelModule.ProjectileCount;
    }

    private void SpawnBullet()
    {
        Quaternion rotation = GetSpreadRotation();
        GameObject bullet = Instantiate(ActiveBulletPrefab, _projectileSpawner.position, rotation);
        
        if (bullet.TryGetComponent<IProjectile>(out var projectile))
        {
            projectile.Speed *= BulletSpeedMultiplier;
            projectile.Damage *= DamageMultiplier;
        }
    }

    private Quaternion GetSpreadRotation()
    {
        float pitch = Random.Range(-Spread, Spread);
        float yaw = Random.Range(-Spread, Spread);
        return _projectileSpawner.rotation * Quaternion.Euler(pitch, yaw, 0f);
    }

    public override void Recharge()
    {
        _ammoModule.AmountAmmo = _ammoModule.MaxAmmo;
    }
}
