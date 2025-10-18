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
    protected IModifier _modifierModule;
    protected ICatalyst _catalystModule;

    private GameObject _gbjBarrel;
    private GameObject _gbjAmmo;
    private GameObject _gbjModifier;
    private GameObject _gbjCatalyst;

    protected GameObject _bulletPrefab;

    public void ChangeModule(Module newModule)
    {
        if (newModule is IBarrel barrel)
        {
            Destroy(_gbjBarrel);
            _gbjBarrel = Instantiate(newModule.gameObjectOnGun, _transformBarrel.position, _transformBarrel.rotation);
            _gbjBarrel.transform.SetParent(_transformBarrel);
            _barrelModule = _gbjBarrel.GetComponent<IBarrel>();
            return;
        }

        if (newModule is IAmmo ammo)
        {
            Destroy(_gbjAmmo);
            _gbjAmmo = Instantiate(newModule.gameObjectOnGun, _transformAmmo.position, _transformAmmo.rotation);
            _gbjAmmo.transform.SetParent(_transformAmmo);
            _ammoModule = ammo;

            IProjectile ibullet = _bulletPrefab.GetComponent<IProjectile>();
            ibullet.Damage = _ammoModule.Damage;
            ibullet.Speed = _ammoModule.Speed;
            return;
        }
        
          
           /* if (newModule is IModifier modifier)
            {
            Destroy(_gbjModifer);
            _gbjModifer = Instantiate(newModule.gameObjectOnGun, _transformModifier.position, _transformModifier.rotation);
                
            _modifierModule = modifier;
                return;
            }
*/
            
            if (newModule is ICatalyst catalyst)
            {
                Destroy(_gbjCatalyst);
                _gbjCatalyst = Instantiate(newModule.gameObjectOnGun, _transformCatalyst.position, _transformCatalyst.rotation);
                _catalystModule = catalyst;
                _bulletPrefab = _catalystModule.BulletPrefab;
                
                IProjectile ibullet = _bulletPrefab.GetComponent<IProjectile>();
                ibullet.Damage = _ammoModule.Damage;
                ibullet.Speed = _ammoModule.Speed;
                return;
            }
    }
    public virtual void Shoot() { }
    public virtual void Recharge() { }
    
}
