using System.Transactions;
using UnityEngine;

public class Gun : MonoBehaviour, IWeapon
{
    [SerializeField] private Transform _transformBarrel;
    [SerializeField] private Transform _transformAmmo;
    [SerializeField] private Transform _transformModifier;
    [SerializeField] private Transform _transformCatalyst;

    protected IBarrel _barrelModule;
    protected IAmmo _ammoModule;
   // protected IModifier _modifierModule;
   // protected ICatalyst _catalystModule;

    private GameObject _gbjBarrel;
    private GameObject _gbjAmmo;
    private GameObject _gbjModifier;
    private GameObject _gbjCatalyst;

    [SerializeField] protected GameObject _bulletPrefab;

    public void ChangeModule(Module newModule)
    {
        //IBarrel barrel = newModule.GetComponent<IBarrel>();
        if (newModule is IBarrel barrel)
        {
            Destroy(_gbjBarrel);
            _gbjBarrel = Instantiate(newModule.gameObjectOnGun, _transformBarrel.position, _transformBarrel.rotation);
            _gbjBarrel.transform.SetParent(gameObject.transform);
            _barrelModule = _gbjBarrel.GetComponent<IBarrel>();
            return;
        }


       // IAmmo ammo = newModule.GetComponent<IAmmo>();
        if (newModule is IAmmo ammo)
        {
            Destroy(_gbjAmmo);
            _gbjAmmo = Instantiate(newModule.gameObjectOnGun, _transformAmmo.position, Quaternion.identity);
            _gbjAmmo.transform.SetParent(gameObject.transform);
            _ammoModule = ammo;

            IProjectile ibullet = _bulletPrefab.GetComponent<IProjectile>();
            ibullet.Damage = _ammoModule.Damage;
            ibullet.Speed = _ammoModule.Speed;
            return;
        }
        /*
            IModifer modifier = newModule.GetComponent<IModifier>();
            if (modifier != null)
            {
                Destroy(_gbjModifer);
                _gbjModifer = Instantiate(newModule, _transformModifier.position, Quaternion.identity);
                _modifierModule = modifier;
                return;
            }

            ICatalyst  = newModule.GetComponent<IModifier>();
            if (modifier != null)
            {
                Destroy(_gbjModifer);
                _gbjModifer = Instantiate(newModule, _transformModifier.position, Quaternion.identity);
                _modifierModule = modifier;
                return;
            }*/
    }
    public virtual void Shoot() { }
    public virtual void Recharge() { }
    
}
