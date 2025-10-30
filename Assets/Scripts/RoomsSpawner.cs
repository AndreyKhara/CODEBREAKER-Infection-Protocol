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
    private int rand;
    private bool spawned = false;
    private float waitTime = 3f;

    [Header("Generation Parameters")]
    public int maxRooms = 20;
    private static int roomCount = 0;

    [Header("Collision Detection")]
    public float checkRadius = 5f;

    private void Start()
    {
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomsVariants>();
        Invoke("Spawn", 0.2f);
    }

    public void Spawn()
    {
        if (roomCount >= maxRooms)
        {
            Debug.Log("Max rooms reached. Stopping generation.");
            Destroy(gameObject);
            return;
        }

        if (!spawned)
        {
             SpawnRoom();  // Просто спавним комнату
             spawned = true;
        }
    }

    private void SpawnRoom()
    {
        GameObject roomToSpawn = null;

        // Получаем комнату в зависимости от направления
        switch (direction)
        {
            case Direction.Forward:
                roomToSpawn = variants.ForwardRooms[Random.Range(0, variants.ForwardRooms.Length)];
                break;
            case Direction.Back:
                roomToSpawn = variants.BackRooms[Random.Range(0, variants.BackRooms.Length)];
                break;
            case Direction.Right:
                roomToSpawn = variants.RightRooms[Random.Range(0, variants.RightRooms.Length)];
                break;
            case Direction.Left:
                roomToSpawn = variants.LeftRooms[Random.Range(0, variants.LeftRooms.Length)];
                break;
            case Direction.Main:
                roomToSpawn = variants.MainRoom[Random.Range(0, variants.MainRoom.Length)];
                break;
            case Direction.None:
                Debug.LogWarning("Direction is None, spawning a random room.");
                roomToSpawn = variants.MainRoom[Random.Range(0, variants.MainRoom.Length)]; // Или другой вариант по умолчанию
                break;
        }
    

        if (roomToSpawn != null)
    {
        // Улучшенная проверка коллизий - игнорируем самого себя
        Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius);
        bool collision = false;
        
        foreach (Collider collider in colliders)
        {
            // Игнорируем триггеры и самого себя
            if (collider.isTrigger) continue;
            if (collider.gameObject == gameObject) continue;
            
            if (collider.GetComponent<RoomsSpawner>() != null || 
                collider.CompareTag("Room") || 
                collider.CompareTag("RoomPoint"))
            {
                collision = true;
                break;
            }
        }

        if (!collision)
        {
            GameObject newRoom = Instantiate(roomToSpawn, transform.position, roomToSpawn.transform.rotation);
            roomCount++;
            Debug.Log($"Spawned {direction} room. Total rooms: {roomCount}");
        }
        else
        {
            Debug.Log($"Collision detected at {transform.position}. Not spawning room.");
        }

        Destroy(gameObject);
    }
    else
    {
        Debug.LogError("No room to spawn for direction: " + direction);
        Destroy(gameObject);
    }
    } 

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("RoomPoint")) || other.CompareTag("TriggerRoom"))
        {
            Destroy(gameObject);
        }
    }
}
