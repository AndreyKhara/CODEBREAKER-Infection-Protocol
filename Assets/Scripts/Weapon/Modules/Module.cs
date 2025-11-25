using UnityEngine;

public abstract class Module : MonoBehaviour
{
    public GameObject gameObjectOnGun;
    [SerializeField] public RarityIndicator _moduleRarity;
    [SerializeField] private ParticleSystem _ps;
    private Color color;
    public enum RarityIndicator
    {
        Common = 1,
        Rare = 2, 
        Epic = 3,
        Legendary = 4
    }

    protected void Start()
    {
        switch (_moduleRarity)
        {
            case RarityIndicator.Common: color = Color.white; break;
            case RarityIndicator.Rare: color = Color.blue; break;
            case RarityIndicator.Epic: color = Color.magenta; break;
            case RarityIndicator.Legendary: color = new Color(1f, 0.5f, 0f, 1f); break;
        }
        var main = _ps.main;
        main.startColor = new ParticleSystem.MinMaxGradient(color);
    }
}
