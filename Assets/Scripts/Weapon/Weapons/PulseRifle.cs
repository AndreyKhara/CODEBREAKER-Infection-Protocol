using UnityEngine;
using Cysharp.Threading.Tasks;
using System; 

public class PulseRifle : Gun
{
    [SerializeField] private float _delayBullet = 0.1f; 
    [SerializeField] private int _countBullet = 3;

    private bool _isShooting = false; 

    public override void Shoot()
    {
        if (_stopShoot) return;
        if (_isShooting) return;

        if (_ammoModule.AmountAmmo >= _barrelModule.ProjectileCount * _countBullet)
        {
            _isShooting = true; 

            ShootSequence().Forget();

            _ammoModule.AmountAmmo -= _barrelModule.ProjectileCount * _countBullet;
        }
        else
        {
            Recharge();
        }
    }

    private async UniTaskVoid ShootSequence()
    {
        for (int i = 0; i < _barrelModule.ProjectileCount; i++)
        {
            // Логика разброса для КАЖДОЙ пули
            float randomPitch = UnityEngine.Random.Range(-Spread, Spread);
            float randomYaw = UnityEngine.Random.Range(-Spread, Spread);
            Quaternion spreadRotation = Quaternion.Euler(randomPitch, randomYaw, 0f);
            Quaternion finalRotation = _projectileSpawner.rotation * spreadRotation;

            await SpawnBulletBurst(finalRotation);
        }
        _isShooting = false; 
    }
    
    private async UniTask SpawnBulletBurst(Quaternion rotation)
    {
       for (int j = 0; j < _countBullet; j++)
       {
            // Создаем пулю (используем ActiveBulletPrefab для поддержки рикошета)
            GameObject bullet = Instantiate(ActiveBulletPrefab, _projectileSpawner.position, rotation);
            
            // Применяем глитч-множители
            IProjectile projectile = bullet.GetComponent<IProjectile>();
            if (projectile != null)
            {
                projectile.Speed *= BulletSpeedMultiplier;
                projectile.Damage *= DamageMultiplier;
            }
            
            if (j < _countBullet - 1)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_delayBullet), ignoreTimeScale: false);
            }
            AddInstability();
       }
    }

    public override void Recharge()
    {
        _ammoModule.AmountAmmo = _ammoModule.MaxAmmo;
        Debug.Log("Recharge PulseRifle");
    }
}
