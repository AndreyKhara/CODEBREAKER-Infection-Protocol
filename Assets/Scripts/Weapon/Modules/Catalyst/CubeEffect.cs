using UnityEngine;

public class CubeEffect : Module, ICatalyst
{
    [SerializeField] private GameObject _prefabBullet;

    public GameObject BulletPrefab
    {
        get => _prefabBullet;
    }
}
