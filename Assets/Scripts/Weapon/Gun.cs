using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System; 

public class Gun : MonoBehaviour, IWeapon
{
    public float Spread{
        get => _spreadAngle;
        set => _spreadAngle = value;
    }
    public bool _stopShoot = false;

    public float _instability = 0f;
    [SerializeField] private GameObject _weaponUI;
    [SerializeField] private float _addInstabilityAmount = 5f;

    //[SerializeField] private Transform _transformBarrel;
    //[SerializeField] private Transform _transformAmmo;
    //[SerializeField] private Transform _transformCatalyst;

    [SerializeField] private Module _barrelStock;
    [SerializeField] private Module _ammoStock;
    [SerializeField] private Module _catalystStock;

    [SerializeField] protected Transform _projectileSpawner;

    private GlitchManager _glitchManager;

    // Glitch system properties
    [Header("Glitch System")]
    [SerializeField] private GameObject _ricochetBulletPrefab;
    
    /// <summary>
    /// Флаг для RicochetMalfunctionGlitch - использовать рикошетные пули
    /// </summary>
    public bool UseRicochetBullet { get; set; } = false;
    
    /// <summary>
    /// Флаг для TriggerLockGlitch - автоматическая стрельба
    /// </summary>
    public bool IsAutoFireLocked { get; set; } = false;
    
    /// <summary>
    /// Множитель скорости пули для SlowMotionBulletsGlitch
    /// </summary>
    public float BulletSpeedMultiplier { get; set; } = 1f;
    
    /// <summary>
    /// Множитель урона для глитчей (Overcharge, SlowMotion)
    /// </summary>
    public float DamageMultiplier { get; set; } = 1f;
    
    /// <summary>
    /// Публичный доступ к модулю патронов для глитчей
    /// </summary>
    public IAmmo AmmoModule => _ammoModule;
    
    /// <summary>
    /// Публичный доступ к рикошетному префабу
    /// </summary>
    public GameObject RicochetBulletPrefab => _ricochetBulletPrefab;
    
    /// <summary>
    /// Получить текущий активный префаб пули (обычный или рикошетный)
    /// </summary>
    public GameObject ActiveBulletPrefab => UseRicochetBullet && _ricochetBulletPrefab != null 
        ? _ricochetBulletPrefab 
        : _bulletPrefab;

    protected IBarrel _barrelModule;
    protected IAmmo _ammoModule;
    protected IModifier _modifierModule;
    protected ICatalyst _catalystModule;

    private GameObject _gbjBarrel;
    private GameObject _gbjAmmo;
    private GameObject _gbjModifier;
    private GameObject _gbjCatalyst;

    private float _spreadAngle = 0.1f;

    protected GameObject _bulletPrefab;
    
    private void Start()
    {
        ChangeModule(_barrelStock);
        ChangeModule(_catalystStock);
        ChangeModule(_ammoStock);

        _glitchManager = new GlitchManager(this);
    }

    private void Update()
    {
        // TriggerLockGlitch - автоматическая стрельба
        if (IsAutoFireLocked)
        {
            Shoot();
        }
    }

    private void OnEnable()
    {
        _weaponUI.SetActive(true);
    }

    public void ChangeModule(Module newModule)
    {
        if (newModule is IBarrel barrel)
        {
            //Destroy(_gbjBarrel);
            //_gbjBarrel = Instantiate(newModule.gameObjectOnGun, _transformBarrel.position, _transformBarrel.rotation);
            //_gbjBarrel.transform.SetParent(_transformBarrel);
            _spreadAngle = barrel.Spread;
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
            // _gbjCatalyst.transform.SetParent(_transformCatalyst);
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

    protected void AddInstability()
    {
        _instability += _addInstabilityAmount;
        if (_instability >= 100)
        {
           _glitchManager.GlitchEffect();
           _instability = 30;
        }
    }

    private void OnDisable()
    {
        _weaponUI.SetActive(false);
    }
    
    public virtual void Shoot() { 
        
    }
    public virtual void Recharge() { }
    
}
