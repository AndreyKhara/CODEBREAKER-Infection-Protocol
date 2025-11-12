using UnityEngine;

public class CorridorEnd : MonoBehaviour
{
    [Header("Connection Settings")]
    public float connectionCheckRadius = 2f;
    
    [Header("Wall Spawn Point")]
    public Transform wallSpawnPoint;
    
    private bool isConnected = false;

    private void Start()
    {
        Invoke("CheckConnection", 0.5f);
    }

    private void CheckConnection()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, connectionCheckRadius);
        
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject == gameObject) continue;
            
            CorridorEnd otherCorridorEnd = collider.GetComponent<CorridorEnd>();
            if (otherCorridorEnd != null && otherCorridorEnd.gameObject != this.gameObject) 
            {
                if (transform.parent != otherCorridorEnd.transform.parent)
                {
                    isConnected = true;
                    break;
                }
            }
        }
    }

    public bool IsConnected()
    {
        return isConnected;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = isConnected ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, connectionCheckRadius);
        
        if (wallSpawnPoint != null)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(wallSpawnPoint.position, 0.3f);
        }
    }
}