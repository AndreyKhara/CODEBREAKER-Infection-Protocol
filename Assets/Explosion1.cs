using UnityEngine;

public class Explosion1 : MonoBehaviour
{
    [SerializeField] private float aoeDamage = 10f; 
   private void OnTriggerEnter(Collider other)
    {
        IHealth health = other.GetComponent<IHealth>();
        if (health != null)
        {
            health.TakeDamage(aoeDamage);
        }
    }
    
}
