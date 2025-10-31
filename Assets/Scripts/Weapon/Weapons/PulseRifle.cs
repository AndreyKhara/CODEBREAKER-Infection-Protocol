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
            float _spreadAngle = _barrelModule.Spread;
            // Логика разброса для КАЖДОЙ пули
            float randomPitch = UnityEngine.Random.Range(-_spreadAngle, _spreadAngle);
            float randomYaw = UnityEngine.Random.Range(-_spreadAngle, _spreadAngle);
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
            Instantiate(_bulletPrefab, _projectileSpawner.position, rotation);
            if (j < _countBullet - 1)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_delayBullet), ignoreTimeScale: false);
            }
       }
    }

    public override void Recharge()
    {
        _ammoModule.AmountAmmo = _ammoModule.MaxAmmo;
        Debug.Log("Recharge PulseRifle");
    }
}
