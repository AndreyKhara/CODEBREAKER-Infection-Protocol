using UnityEngine;

public class LevelCleanupManager : MonoBehaviour
{
    [Header("Cleanup Settings")]
    public bool autoCleanup = true;
    public float cleanupDelay = 3f;
    
    private void Start()
    {
        if (autoCleanup)
        {
            Invoke("ForceCleanupWithCheck", cleanupDelay);
        }
    }
    
    [ContextMenu("Force Cleanup Now")]
    public void ForceCleanupWithCheck()
    {
        RoomsSpawner.CleanupUnconnectedCorridors();
    }
    
    [ContextMenu("Check Corridor Status")]
    public void CheckAllCorridors()
    {
        CorridorEnd[] allCorridors = FindObjectsByType<CorridorEnd>(FindObjectsSortMode.None);
        int connected = 0;
        int unconnected = 0;
        
        foreach (CorridorEnd corridor in allCorridors)
        {
            if (corridor.IsConnected())
                connected++;
            else
                unconnected++;
        }
        
        Debug.Log($"Corridors - Total: {allCorridors.Length}, Connected: {connected}, Unconnected: {unconnected}");
    }
}