using System.Transactions;
using UnityEngine;

public class Gun : MonoBehaviour, IWeapon
{
    //[SerializeField] private Transform _transformBarrel;
    //[SerializeField] private Transform _transformAmmo;
    //[SerializeField] private Transform _transformCatalyst;
    [SerializeField] private GameObject _weaoponTexture;
    [SerializeField] private Module _barrelStock;
    [SerializeField] private Module _ammoStock;
    [SerializeField] private Module _catalystStock;

    [SerializeField] protected Transform _projectileSpawner;

    protected IBarrel _barrelModule;
    protected IAmmo _ammoModule;
    protected IModifier _modifierModule;
    protected ICatalyst _catalystModule;

    //private GameObject _gbjBarrel;
    //private GameObject _gbjAmmo;
    //private GameObject _gbjModifier;
    //private GameObject _gbjCatalyst;

    protected GameObject _bulletPrefab;

    private void Start()
    {
        ChangeModule(_barrelStock);
        ChangeModule(_catalystStock);
        ChangeModule(_ammoStock);
        
    }

    private void OnEnable()
    {
        _weaoponTexture.SetActive(true);
    }
    public void ChangeModule(Module newModule)
    {
        Debug.Log("Change Module");
        if (newModule is IBarrel barrel)
        {
            //Destroy(_gbjBarrel);
            // = Instantiate(newModule.gameObjectOnGun, _transformBarrel.position, _transformBarrel.rotation);
            //_gbjBarrel.transform.SetParent(_transformBarrel);
            _barrelModule = barrel;

            return;
        }

        if (newModule is IAmmo ammo)
        {
            //Destroy(_gbjAmmo);
            //_gbjAmmo = Instantiate(newModule.gameObjectOnGun, _transformAmmo.position, _transformAmmo.rotation);
            //_gbjAmmo.transform.SetParent(_transformAmmo);
            _ammoModule = ammo;

            UpdateBullet();

            return;
        }


        if (newModule is ICatalyst catalyst)
        {
            //Destroy(_gbjCatalyst);
            //_gbjCatalyst = Instantiate(newModule.gameObjectOnGun, _transformCatalyst.position, _transformCatalyst.rotation);
            //_gbjCatalyst.transform.SetParent(_transformCatalyst);
            _catalystModule = catalyst;

            _bulletPrefab = _catalystModule.BulletPrefab;

            UpdateBullet();

            return;
        }
    }

    private void UpdateBullet()
    {
        if (_bulletPrefab != null)
        {
            IProjectile ibullet = _bulletPrefab.GetComponent<IProjectile>();
            if (_ammoModule != null)
            {
                // Применяем параметры ТОЛЬКО от Ammo (урон, скорость, дальность)
                ibullet.Damage = _ammoModule.BaseDamage;
                ibullet.Speed = _ammoModule.ProjectileSpeed;
                ibullet.TimeLife = _ammoModule.ProjectileRange;
            }
            else Debug.Log("No ammo module");
        }
        else
        {
            Debug.Log("No prefab bullet");
        }
    }

    private void OnDisable()
    {
        _weaoponTexture.SetActive(false);
    }
    
    public virtual void Shoot() { }
    public virtual void Recharge() { }
    
}
