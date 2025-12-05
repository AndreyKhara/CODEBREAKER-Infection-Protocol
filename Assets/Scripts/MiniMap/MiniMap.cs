using System.Collections.Generic;
using UnityEngine;
public class MiniMap : MonoBehaviour
{
    public static MiniMap Instance;

    [Header("UI References")]
    public GameObject playerIconPrefab;
    public RectTransform mapPanel;
    public RectTransform mapContent; 

    [Header("Размеры на экране")]
    public float cellStepX = 60f; 
    public float cellStepY = 50f; 

    [Header("Размеры в мире")]
    public float worldStepX = 60f; 
    public float worldStepY = 50f; 

    [Header("Corridor  Prefabs")]
    public GameObject bridgeHorizontalPrefab; 
    public GameObject bridgeVerticalPrefab;

    private Dictionary<Vector2Int, MiniMapRoom> rooms = new();
    
    private struct BridgeData { public RectTransform rt; public Vector2 gridCenter; }
    private List<BridgeData> activeBridges = new List<BridgeData>();
    private HashSet<string> builtBridgesIDs = new HashSet<string>();

    private GameObject playerIcon;

    private void Awake() => Instance = this;

    private void Start()
    {
        if (playerIconPrefab != null)
        {
            playerIcon = Instantiate(playerIconPrefab, mapPanel);
            playerIcon.transform.SetAsLastSibling();
            playerIcon.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }

    // КОМНАТЫ 
    public void RegisterRoom(MiniMapRoom room)
    {
        if (rooms.ContainsKey(room.gridPos)) rooms.Remove(room.gridPos);
        rooms[room.gridPos] = room;
    }

    public MiniMapRoom GetRoomAt(Vector2Int pos)
    {
        return rooms.ContainsKey(pos) ? rooms[pos] : null;
    }

    public GameObject CreateIcon(GameObject prefab, Vector2Int gridPos)
    {
        GameObject newIcon = Instantiate(prefab, mapContent);
        
        newIcon.GetComponent<RectTransform>().anchoredPosition = GridToPosition(gridPos);
        
        newIcon.transform.SetAsLastSibling(); 
        return newIcon;
    }

    // КОРИДОРЫ 
    public void RegisterBridge(Vector2Int posA, Vector2Int dir)
    {
        Vector2Int posB = posA + dir;
        string id = GetBridgeID(posA, posB);

        if (builtBridgesIDs.Contains(id)) return;

        builtBridgesIDs.Add(id);
        SpawnBridge(posA, posB, dir);
    }

    private void SpawnBridge(Vector2Int posA, Vector2Int posB, Vector2Int dir)
    {
        GameObject prefab = (dir.x != 0) ? bridgeHorizontalPrefab : bridgeVerticalPrefab;
        if (prefab == null) return;

        GameObject bridgeObj = Instantiate(prefab, mapContent);
        bridgeObj.transform.SetAsFirstSibling(); // Под комнаты

        RectTransform rt = bridgeObj.GetComponent<RectTransform>();
        
        Vector2 gridCenter = (new Vector2(posA.x, posA.y) + new Vector2(posB.x, posB.y)) / 2f;

        activeBridges.Add(new BridgeData { rt = rt, gridCenter = gridCenter });
    }

    private string GetBridgeID(Vector2Int a, Vector2Int b)
    {
        if (a.x < b.x || (a.x == b.x && a.y < b.y)) return $"{a.x},{a.y}|{b.x},{b.y}";
        else return $"{b.x},{b.y}|{a.x},{a.y}";
    }

    public void PlayerEnteredRoom(MiniMapRoom room)
    {
        room.TryDiscover(); 

        UpdateMapPosition(room.gridPos);
    }

    public void UpdateMapPosition(Vector2Int playerGridPos)
    {
        Vector2 centerPixelPos = GridToPosition(playerGridPos);

        foreach (var r in rooms.Values)
        {
            if (r.icon != null)
            {
                Vector2 roomPos = GridToPosition(r.gridPos);
                r.icon.GetComponent<RectTransform>().anchoredPosition = roomPos - centerPixelPos;
            }
        }

        foreach (var bridge in activeBridges)
        {
            if (bridge.rt != null)
            {
                Vector2 bridgePos = new Vector2(bridge.gridCenter.x * cellStepX, bridge.gridCenter.y * cellStepY);
                bridge.rt.anchoredPosition = bridgePos - centerPixelPos;
            }
        }
    }

    public Vector2Int WorldToGrid(Vector3 worldPos)
    {
        return new Vector2Int(
            Mathf.RoundToInt(worldPos.x / worldStepX),
            Mathf.RoundToInt(worldPos.z / worldStepY)
        );
    }
    public Vector2 GridToPosition(Vector2 gridPos)
    {
        return new Vector2(gridPos.x * cellStepX, gridPos.y * cellStepY);
    }
}