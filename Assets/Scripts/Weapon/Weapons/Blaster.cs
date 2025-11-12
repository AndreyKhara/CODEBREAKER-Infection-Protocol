using System.Reflection;
using UnityEngine;

public class Blaster : Gun
{
    public override void Shoot()
    {
        // Проверяем, достаточно ли патронов для всех выстрелов за одно нажатие
        if (_ammoModule.AmountAmmo >= _barrelModule.ProjectileCount)
        {
            for (int i = 0; i < _barrelModule.ProjectileCount; i++)
            {
                float _spreadAngle = _barrelModule.Spread;
                // Логика разброса для КАЖДОЙ пули
                float randomPitch = Random.Range(-_spreadAngle, _spreadAngle);
                float randomYaw = Random.Range(-_spreadAngle, _spreadAngle);
                Quaternion spreadRotation = Quaternion.Euler(randomPitch, randomYaw, 0f);
                Quaternion finalRotation = _projectileSpawner.rotation * spreadRotation;

                // Создаем пулю
                Instantiate(_bulletPrefab, _projectileSpawner.position, finalRotation);
                Debug.Log(_bulletPrefab);
                
            }
            _ammoModule.AmountAmmo -= _barrelModule.ProjectileCount;
        }
        else
        {
            Recharge();
        }
    }

    public override void Recharge()
    {
        _ammoModule.AmountAmmo = _ammoModule.MaxAmmo;
        Debug.Log("Recharge blaster");
    }
}
