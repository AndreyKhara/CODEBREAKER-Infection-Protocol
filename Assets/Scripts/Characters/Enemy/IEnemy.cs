using UnityEngine;

namespace CDB.Character.Enemy
{
    public interface IEnemy
    {
        float Speed { get; set; }
        float Damage { get; set; }
        void Attack();


    }
}
