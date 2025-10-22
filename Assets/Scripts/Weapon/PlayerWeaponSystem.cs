using UnityEngine;
using System.Collections.Generic;
using Zenject;
using UnityEngine.InputSystem;
using System.Net.NetworkInformation;
using System.Reflection;

namespace CDB.Input
{
    public class PlayerWeaponSystem : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _weapons;
        public IWeapon iCurrentWeapon;
        private int _indexWeapon = 0; 

        private InputAction _shootAction;
        private InputAction _changeWeaponAction;
        private InputAction _rechargeAction;

        [Inject]
        private PlayerInput _playerInput;

        private void Awake()
        {
            _shootAction = _playerInput.actions["Shoot"];
            _changeWeaponAction = _playerInput.actions["ChangeWeapon"];
            _rechargeAction = _playerInput.actions["Recharge"];

            _shootAction.performed += ShootPressed;
            _rechargeAction.performed += RechargePressed;
            _changeWeaponAction.performed += ChangeWeapon;

            iCurrentWeapon = _weapons[_indexWeapon].GetComponent<IWeapon>();
        }

        private void ChangeWeapon(InputAction.CallbackContext context)
        {
            Vector2 scrollVector = context.ReadValue<Vector2>();
            int scrollAmount = Mathf.RoundToInt(scrollVector.y);

            if (scrollAmount > 0)
            {
                SwitchToNextWeapon();
            }
            else if (scrollAmount < 0)
            {
                SwitchToBackWeapon(); 
            }
        }

        private void SwitchToNextWeapon()
        {
            _indexWeapon++;

            if (_indexWeapon > _weapons.Count-1)
            {
                _indexWeapon = 0;
            }

            ActivateWeapon(_indexWeapon);
        }

        private void SwitchToBackWeapon()
        {
             _indexWeapon--;

            if (_indexWeapon < 0)
            {
                _indexWeapon = _weapons.Count - 1;
            }

            ActivateWeapon(_indexWeapon);
        }


        private void ActivateWeapon(int index)
        {
            for (int i = 0; i < _weapons.Count; i++)
            {
                _weapons[i].SetActive(i == index);

            }
            iCurrentWeapon = _weapons[_indexWeapon].GetComponent<IWeapon>();
        }
        private void ShootPressed(InputAction.CallbackContext context)
        {
            iCurrentWeapon.Shoot();
        }

        private void RechargePressed(InputAction.CallbackContext context)
        {
            iCurrentWeapon.Recharge();
        }

    }
}