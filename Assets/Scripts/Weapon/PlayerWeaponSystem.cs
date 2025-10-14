using UnityEngine;
using System.Collections.Generic;
using Zenject;
using UnityEngine.InputSystem;



namespace CDB.Input
{
    public class PlayerWeaponSystem : MonoBehaviour
    {
        public List<GameObject> weapons;
        //[SerializeField] private Blaster currentWeapon;
        private IWeapon _iCurrentWeapon;
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

            _iCurrentWeapon = weapons[_indexWeapon].GetComponent<IWeapon>();
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

            if (_indexWeapon > weapons.Count-1)
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
                _indexWeapon = weapons.Count - 1;
            }

            ActivateWeapon(_indexWeapon);
        }


        void ActivateWeapon(int index)
        {
            for (int i = 0; i < weapons.Count; i++)
            {
                weapons[i].SetActive(i == index);

            }
            _iCurrentWeapon = weapons[_indexWeapon].GetComponent<IWeapon>();
        }
        private void ShootPressed(InputAction.CallbackContext context)
        {
            _iCurrentWeapon.Shoot();
        }

        private void RechargePressed(InputAction.CallbackContext context)
        {
            _iCurrentWeapon.Recharge();
        }
    }
}