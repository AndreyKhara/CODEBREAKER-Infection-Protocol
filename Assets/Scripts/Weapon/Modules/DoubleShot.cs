using UnityEngine;

public class DoubleShot : Module, IBarrel
{
    [SerializeField] private float _spread = 1f;
    [SerializeField] private int _projectileCount = 1;
    
    public float Spread
    {
        get => _spread;
        private set => _spread = value;
    }

    public int ProjectileCount
    {
        get => _projectileCount;
        private set => _projectileCount = value;
    }

    public Transform GunPoint
    {
        get => transform;
    }
}
