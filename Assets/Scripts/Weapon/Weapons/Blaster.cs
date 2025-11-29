using System.Reflection;
using UnityEngine;

public class Blaster : Gun
{
    public override void Shoot()
    {
        if (_stopShoot) return;
        // Проверяем, достаточно ли патронов для всех выстрелов за одно нажатие
        if (_ammoModule.AmountAmmo >= _barrelModule.ProjectileCount)
        {
            for (int i = 0; i < _barrelModule.ProjectileCount; i++)
            {
                // Логика разброса для КАЖДОЙ пули
                float randomPitch = Random.Range(-Spread, Spread);
                float randomYaw = Random.Range(-Spread, Spread);
                Quaternion spreadRotation = Quaternion.Euler(randomPitch, randomYaw, 0f);
                Quaternion finalRotation = _projectileSpawner.rotation * spreadRotation;

                // Создаем пулю
                Instantiate(_bulletPrefab, _projectileSpawner.position, finalRotation);
                AddInstability();
                //Debug.Log(_bulletPrefab);
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
