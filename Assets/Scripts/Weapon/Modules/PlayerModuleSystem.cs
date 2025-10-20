using System.Security.Cryptography;
using UnityEngine;
using CDB.Input;


public class PlayerModuleSystem : MonoBehaviour
{
    [SerializeField] private PlayerWeaponSystem _playerWeaponSystem;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("On Collision");
        // TO DO
        if (other.CompareTag("Module"))
        {
            Module newModule = other.GetComponent<Module>();

            if (newModule == null) return;

            _playerWeaponSystem.iCurrentWeapon.ChangeModule(newModule);

            Destroy(other.gameObject);
        }
    }
}
