using UnityEngine;
namespace CDB.Character
{
    public interface IHealth
    {
        float MaxHealth { get; set; }
        float CurrentHealth { get; set; }
        void TakeDamage(float damageAmount);
        void Heal(float healAmount);
        void Death();
    }
}