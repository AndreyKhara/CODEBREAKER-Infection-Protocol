using UnityEngine;
using System.Collections.Generic;
using Zenject;
using UnityEngine.InputSystem;


namespace CDB.Input
{
    public class PlayerWeaponSystem : MonoBehaviour
    {
        public List<IWeapon> weapon;
        [SerializeField] private Blaster currentWeapon;

        private InputAction _shootAction;
        private InputAction _changeWeaponAction;
        private InputAction _rechargeAction;

        private IWeapon _iweapon;

        [Inject]
        private PlayerInput _playerInput;

        private void Awake()
        {
            _shootAction = _playerInput.actions["Shoot"];
            _changeWeaponAction = _playerInput.actions["ChangeWeapon"];
            _rechargeAction = _playerInput.actions["Recharge"];

            _shootAction.performed += ShootPressed;
            _rechargeAction.performed += RechargePressed;

            _iweapon = GetComponent<IWeapon>();
        }

        private void ChangeWeapon(InputAction.CallbackContext context)
        {
            
        }

        private void ShootPressed(InputAction.CallbackContext context)
        {
            currentWeapon.Shoot();
        }

        private void RechargePressed(InputAction.CallbackContext context)
        {
            currentWeapon.Recharge();
        }
    }
}