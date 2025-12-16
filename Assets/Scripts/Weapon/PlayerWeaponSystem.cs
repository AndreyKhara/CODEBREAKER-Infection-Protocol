using UnityEngine;
using System.Collections.Generic;
using Zenject;
using UnityEngine.InputSystem;

namespace CDB.Input
{
    public class PlayerWeaponSystem : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _weapons;
        [SerializeField] private AudioSource _audioSourceGun;

        public IWeapon iCurrentWeapon;
        private int _indexWeapon = 0;

        private InputAction _shootAction;
        private InputAction _changeWeaponAction;
        private InputAction _rechargeAction;

        [Inject]
        private PlayerInput _playerInput;
        [Inject]
        private Animator _animatorHand;
        

        private void Awake()
        {
            _shootAction = _playerInput.actions["Shoot"];
            _changeWeaponAction = _playerInput.actions["ChangeWeapon"];
            _rechargeAction = _playerInput.actions["Recharge"];

            iCurrentWeapon = _weapons[_indexWeapon].GetComponent<IWeapon>();
        }

        private void OnEnable()
        {
            _shootAction.performed += ShootPressed;
            _rechargeAction.performed += RechargePressed;
            _changeWeaponAction.performed += ChangeWeapon;

            _shootAction.canceled += ShootCanceled;
        }

        private void OnDisable()
        {
            _shootAction.performed -= ShootPressed;
            _rechargeAction.performed -= RechargePressed;
            _changeWeaponAction.performed -= ChangeWeapon;
        }

        private void OnDestroy()
        {
            _shootAction.performed -= ShootPressed;
            _rechargeAction.performed -= RechargePressed;
            _changeWeaponAction.performed -= ChangeWeapon;
        }


        private void ChangeWeapon(InputAction.CallbackContext context)
        {
            Vector2 scrollVector = context.ReadValue<Vector2>();
            int scrollAmount = Mathf.RoundToInt(scrollVector.y);

            if (scrollAmount > 0)
                SwitchToNextWeapon();
            else if (scrollAmount < 0)
                SwitchToBackWeapon();
        }

        private void SwitchToNextWeapon()
        {
            _indexWeapon++;

            if (_indexWeapon >= _weapons.Count)
                _indexWeapon = 0;

            ActivateWeapon(_indexWeapon);
        }

        private void SwitchToBackWeapon()
        {
            _indexWeapon--;

            if (_indexWeapon < 0)
                _indexWeapon = _weapons.Count - 1;

            ActivateWeapon(_indexWeapon);
        }

        private void ActivateWeapon(int index)
        {
            for (int i = 0; i < _weapons.Count; i++)
                _weapons[i].SetActive(i == index);

            iCurrentWeapon = _weapons[index].GetComponent<IWeapon>();
        }

        private void ShootPressed(InputAction.CallbackContext context)
        {
            if (iCurrentWeapon == null) return;
            _animatorHand.SetBool("IsShoot", true);
            _audioSourceGun.clip = iCurrentWeapon.AudioShoot[UnityEngine.Random.Range(0, iCurrentWeapon.AudioShoot.Length)];
            _audioSourceGun.Play();
            iCurrentWeapon.Shoot();
        }

        private void ShootCanceled(InputAction.CallbackContext context)
        {
            _animatorHand.SetBool("IsShoot", false);
        }

        private void RechargePressed(InputAction.CallbackContext context)
        {
            if (iCurrentWeapon == null) return;
            iCurrentWeapon.Recharge();
        }
    }
}
