using UnityEngine;

public class ImpactCharge : Module, ICatalyst
{
    [SerializeField] private GameObject _prefabBullet;
    
    public GameObject BulletPrefab => _prefabBullet;
    
}
