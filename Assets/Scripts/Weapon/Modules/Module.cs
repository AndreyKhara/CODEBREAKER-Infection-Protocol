using UnityEngine;

public abstract class Module : MonoBehaviour
{
    public GameObject gameObjectOnGun;
    
    [Header("Module Properties")]
    [SerializeField] protected ModuleRarity _rarity = ModuleRarity.Common;
    
    public ModuleRarity Rarity => _rarity;
    
    /// <summary>
    /// Множитель бонусов в зависимости от редкости
    /// </summary>
    public float RarityMultiplier
    {
        get
        {
            return _rarity switch
            {
                ModuleRarity.Common => 1.0f,
                ModuleRarity.Uncommon => 1.15f,
                ModuleRarity.Rare => 1.3f,
                ModuleRarity.Epic => 1.5f,
                ModuleRarity.Legendary => 1.75f,
                ModuleRarity.Exotic => 2.0f,
                _ => 1.0f
            };
        }
    }
}
