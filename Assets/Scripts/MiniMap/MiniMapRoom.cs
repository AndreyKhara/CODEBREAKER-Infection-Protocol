using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MiniMapRoom : MonoBehaviour
{
    [Header("Префабы иконок")]
    public GameObject visitedPrefab; 
    public GameObject knownPrefab;  

    [HideInInspector]
    public GameObject icon;
    
    public bool isVisited = false;
    public bool isKnown = false;
    
    public Vector2Int gridPos; 
    private Dictionary<Vector2Int, CorridorEnd> physicalExits = new Dictionary<Vector2Int, CorridorEnd>();

    private void Awake()
    {
        MapPhysicalExits();
    }

    private void Start()
    {
        if (MiniMap.Instance != null)
        {
            gridPos = MiniMap.Instance.WorldToGrid(transform.position);
            MiniMap.Instance.RegisterRoom(this);
        }
    }
    public void TryDiscover()
    {
        if (Time.timeSinceLevelLoad < 1.0f)
        {
            StartCoroutine(DiscoverRoutine());
        }
        else
        {
            DiscoverImmediate();
        }
    }

    private IEnumerator DiscoverRoutine()
    {
        if (isVisited) yield break;
        yield return new WaitForSeconds(0.6f); 
        DiscoverImmediate();
    }

     private void DiscoverImmediate()
    {
        if (isVisited) return; 

        isVisited = true;
        isKnown = true;

        SpawnIcon(visitedPrefab);
        ShowAllExits();     
        RevealNeighbors();  
        
        if (icon != null) icon.transform.SetAsLastSibling();

        if (MiniMap.Instance != null)
        {
            MiniMap.Instance.UpdateMapPosition(gridPos);
        }
    }

    //Соседние комнаты
    public void RevealAsKnown()
    {
        if (isVisited || isKnown) return;
        
        isKnown = true;
        SpawnIcon(knownPrefab);
    }

    private void SpawnIcon(GameObject prefabToSpawn)
    {
        if (icon != null) Destroy(icon);

        if (MiniMap.Instance != null)
        {
            icon = MiniMap.Instance.CreateIcon(prefabToSpawn, gridPos);
        }
    }

    private void ShowAllExits()
    {
        Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };

        foreach (var dir in directions)
        {
            if (HasConnectedPath(dir))
            {
                MiniMap.Instance.RegisterBridge(gridPos, dir);
            }
        }
    }

    private void RevealNeighbors()
    {
        Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };

        foreach (var dir in directions)
        {
            if (!HasConnectedPath(dir)) continue;

            Vector2Int neighborPos = gridPos + dir;
            MiniMapRoom neighbor = MiniMap.Instance.GetRoomAt(neighborPos);

            if (neighbor != null && !neighbor.isVisited)
            {
                neighbor.RevealAsKnown();
            }
        }
    }

    private void MapPhysicalExits()
    {
        CorridorEnd[] exits = GetComponentsInChildren<CorridorEnd>();
        foreach (var exit in exits)
        {
            Vector3 dirVector = exit.transform.position - transform.position;
            Vector2Int gridDir = Vector2Int.zero;

            if (dirVector.z > 1f) gridDir = Vector2Int.up;
            else if (dirVector.z < -1f) gridDir = Vector2Int.down;
            else if (dirVector.x > 1f) gridDir = Vector2Int.right;
            else if (dirVector.x < -1f) gridDir = Vector2Int.left;

            if (gridDir != Vector2Int.zero) physicalExits[gridDir] = exit;
        }
    }

    private bool HasConnectedPath(Vector2Int dir)
    {
        if (!physicalExits.ContainsKey(dir)) return false;
        CorridorEnd exit = physicalExits[dir];
        
        if (exit == null) return false;

        if (!exit.IsConnected()) return false;

        return true;
    }
}