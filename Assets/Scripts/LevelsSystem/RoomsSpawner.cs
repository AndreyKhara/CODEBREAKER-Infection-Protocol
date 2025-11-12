using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsSpawner : MonoBehaviour
{
    public Direction direction;

    public enum Direction
    {
        Forward,
        Back,
        Right,
        Left,
        Main,
        None
    }

    private RoomsVariants variants;
    private bool spawned = false;

    [Header("Generation Parameters")]
    public int maxRooms = 20;
    private static int roomCount = 0;
    private static bool isGenerating = true;

    [Header("Collision Detection")]
    public float checkRadius = 5f;

    [Header("Wall Settings")]
    public GameObject wallPrefab;

    private static List<GameObject> allCorridorEnds = new List<GameObject>();
    private static List<RoomsSpawner> allSpawners = new List<RoomsSpawner>();
    private static List<CorridorEnd> allCorridorEndComponents = new List<CorridorEnd>();
    
    private static GameObject staticWallPrefab;

    private void Start()
    {
        allSpawners.Add(this);
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomsVariants>();
        
        if (wallPrefab != null && staticWallPrefab == null)
        {
            staticWallPrefab = wallPrefab;
        }
        
        Invoke("Spawn", 0.2f);
    }

    public void Spawn()
    {
        if (!isGenerating)
        {
            Destroy(gameObject);
            return;
        }

        if (roomCount >= maxRooms)
        {
            isGenerating = false;
            StartCoroutine(CleanupCorridorsAfterDelay(1f));
            Destroy(gameObject);
            return;
        }

        if (!spawned)
        {
            if (!HasCollision())
            {
                SpawnRoom();
                spawned = true;
                roomCount++;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private bool HasCollision()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius);
        
        foreach (Collider collider in colliders)
        {
            if (collider.isTrigger) continue;
            if (collider.gameObject == gameObject) continue;
            
            if (collider.GetComponent<RoomsSpawner>() != null || 
                collider.CompareTag("Rooms") || 
                collider.CompareTag("RoomPoint"))
            {
                return true;
            }
        }
        return false;
    }

    private void SpawnRoom()
    {
        GameObject roomToSpawn = GetRoomToSpawn();

        if (roomToSpawn != null)
        {
            GameObject newRoom = Instantiate(roomToSpawn, transform.position, roomToSpawn.transform.rotation);
            
            if (!newRoom.CompareTag("Rooms"))
            {
                newRoom.tag = "Rooms";
            }
            
            RegisterCorridorEnds(newRoom);
        }
    }

    private GameObject GetRoomToSpawn()
    {
        switch (direction)
        {
            case Direction.Forward:
                return variants.ForwardRooms[Random.Range(0, variants.ForwardRooms.Length)];
            case Direction.Back:
                return variants.BackRooms[Random.Range(0, variants.BackRooms.Length)];
            case Direction.Right:
                return variants.RightRooms[Random.Range(0, variants.RightRooms.Length)];
            case Direction.Left:
                return variants.LeftRooms[Random.Range(0, variants.LeftRooms.Length)];
            case Direction.Main:
                return variants.MainRoom[Random.Range(0, variants.MainRoom.Length)];
            default:
                return variants.MainRoom[Random.Range(0, variants.MainRoom.Length)];
        }
    }

    private void RegisterCorridorEnds(GameObject room)
    {
        CorridorEnd[] corridorEnds = room.GetComponentsInChildren<CorridorEnd>();
        foreach (CorridorEnd corridorEnd in corridorEnds)
        {
            if (!allCorridorEnds.Contains(corridorEnd.gameObject))
            {
                allCorridorEnds.Add(corridorEnd.gameObject);
                allCorridorEndComponents.Add(corridorEnd);
            }
        }
    }

    private IEnumerator CleanupCorridorsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        CleanupUnconnectedCorridors();
        
        foreach (RoomsSpawner spawner in allSpawners.ToArray())
        {
            if (spawner != null && spawner.gameObject != null)
            {
                Destroy(spawner.gameObject);
            }
        }
        allSpawners.Clear();
    }

    public static void CleanupUnconnectedCorridors()
    {
        if (allCorridorEnds.Count == 0) return;

        List<CorridorEnd> unconnectedCorridorEnds = new List<CorridorEnd>();

        foreach (CorridorEnd corridorComponent in allCorridorEndComponents.ToArray())
        {
            if (corridorComponent == null) continue;

            if (!corridorComponent.IsConnected())
            {
                unconnectedCorridorEnds.Add(corridorComponent);
            }
        }

        foreach (CorridorEnd unconnectedCorridor in unconnectedCorridorEnds)
        {
            if (unconnectedCorridor == null) continue;

            Transform parentRoomTransform = unconnectedCorridor.transform.parent;

            if (parentRoomTransform != null)
            {
                SpawnWallAtCorridorStart(unconnectedCorridor);
                Destroy(parentRoomTransform.gameObject);
            }
        }

        allCorridorEnds.Clear();
        allCorridorEndComponents.Clear();
    }

    private static void SpawnWallAtCorridorStart(CorridorEnd corridorEnd)
{
    if (corridorEnd == null) return;
    
    if (corridorEnd.transform.parent == null) return;

    if (staticWallPrefab != null)
    {
        Vector3 wallPosition;
        
        if (corridorEnd.wallSpawnPoint != null)
        {
            wallPosition = corridorEnd.wallSpawnPoint.position;
        }
        else
        {
            wallPosition = corridorEnd.transform.position;
            Vector3 direction = corridorEnd.transform.forward;
            wallPosition -= direction * 2f;
        }
        
        Quaternion wallRotation = GetWallRotation(corridorEnd.transform);
        
        GameObject wall = Instantiate(staticWallPrefab, wallPosition, wallRotation);
        
        // ИСПРАВЛЕНИЕ: Создаем контейнер WallEnd на корневом уровне сцены
        GameObject wallEndContainer = GameObject.Find("WallEnd");
        if (wallEndContainer == null)
        {
            wallEndContainer = new GameObject("WallEnd");
            // Важно: устанавливаем родителя в null, чтобы контейнер был на корневом уровне
            wallEndContainer.transform.SetParent(null);
        }
        
        // Убеждаемся, что контейнер не стал дочерним какой-либо комнаты
        if (wallEndContainer.transform.parent != null)
        {
            wallEndContainer.transform.SetParent(null);
        }
        
        wall.transform.SetParent(wallEndContainer.transform);
    }
}

    private static Quaternion GetWallRotation(Transform corridorEndTransform)
    {
        string name = corridorEndTransform.name.ToLower();
        string parentName = corridorEndTransform.parent.name.ToLower();

        if (name.Contains("right") || parentName.Contains("right"))
        {
            return Quaternion.Euler(0, 0, 0);
        }
        else if (name.Contains("left") || parentName.Contains("left"))
        {
            return Quaternion.Euler(0, 180, 0);
        }
        else if (name.Contains("back") || parentName.Contains("back"))
        {
            return Quaternion.Euler(0, 90, 0);
        }
        else if (name.Contains("forward") || parentName.Contains("forward") || name.Contains("front"))
        {
            return Quaternion.Euler(0, 270, 0);
        }

        Vector3 localPos = corridorEndTransform.localPosition;
        float absX = Mathf.Abs(localPos.x);
        float absZ = Mathf.Abs(localPos.z);

        if (absX > absZ)
        {
            return localPos.x > 0 ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
        }
        else
        {
            return localPos.z > 0 ? Quaternion.Euler(0, 270, 0) : Quaternion.Euler(0, 90, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!spawned && (other.CompareTag("RoomPoint") || 
            other.CompareTag("Rooms") || 
            other.GetComponent<RoomsSpawner>() != null))
        {
            Destroy(gameObject);
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void ResetStaticVariables()
    {
        roomCount = 0;
        isGenerating = true;
        allCorridorEnds.Clear();
        allSpawners.Clear();
        allCorridorEndComponents.Clear();
        staticWallPrefab = null;
    }

    private void OnDestroy()
    {
        allSpawners.Remove(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = spawned ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}