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
                Quaternion finalRotation = _barrelModule.GunPoint.rotation * spreadRotation;

                // Создаем пулю
                Instantiate(_bulletPrefab, _barrelModule.GunPoint.position, finalRotation);

                // Если вы хотите, чтобы пули немного расходились в стороны,
                // можно добавить небольшой сдвиг к позиции для каждого выстрела.
                // Например, для двойного выстрела:
                // if (_shotsPerTriggerPull == 2) {
                //     Vector3 offset = _barrelTransform.right * (i == 0 ? -0.1f : 0.1f); // Сдвиг влево/вправо
                //     Instantiate(_bulletPrefab, _barrelTransform.position + offset, finalRotation);
                // } else {
                //     Instantiate(_bulletPrefab, _barrelTransform.position, finalRotation);
                // }
                // Но для начала, просто из одной точки.
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
        Debug.Log("Recharge");
    }
}
