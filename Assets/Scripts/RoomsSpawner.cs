using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsSpawner : MonoBehaviour
{
    public Direction direction;
    public bool spawnCorridorFirst = true;
    public bool isCorridor = false;

    public enum Direction
    {
        Forward,
        Back,
        Right,
        Left,
        Main,
        None
    }

    private RoomsVariant variants;
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
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomsVariant>();
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
            if (spawnCorridorFirst && !isCorridor)
            {
                SpawnCorridor();
            }
            else
            {
                SpawnRoom();
            }
            spawned = true;
        }
    }

    private void SpawnCorridor()
    {
        if (variants == null) return;

        GameObject corridorToSpawn = null;

        switch (direction)
        {
            case Direction.Forward:
                corridorToSpawn = variants.forwardCorridor;
                break;
            case Direction.Back:
                corridorToSpawn = variants.backCorridor;
                break;
            case Direction.Right:
                corridorToSpawn = variants.rightCorridor;
                break;
            case Direction.Left:
                corridorToSpawn = variants.leftCorridor;
                break;
        }

        if (corridorToSpawn != null)
        {
            GameObject existingRoomOrCorridor = CheckForExistingRoomOrCorridor(transform.position, checkRadius);

            if (existingRoomOrCorridor == null)
            {
                Instantiate(corridorToSpawn, transform.position, transform.rotation);
            }
            else
            {
                Destroy(gameObject); //Уничтожаем только новый спавнер, если есть коллизия
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void SpawnRoom()
    {
        if (variants == null) return;

        GameObject roomObj = null;

        if (direction == Direction.Forward && variants.forwardRooms != null && variants.forwardRooms.Length > 0)
        {
            rand = Random.Range(0, variants.forwardRooms.Length);
            roomObj = variants.forwardRooms[rand];
        }
        else if (direction == Direction.Back && variants.backRooms != null && variants.backRooms.Length > 0)
        {
            rand = Random.Range(0, variants.backRooms.Length);
            roomObj = variants.backRooms[rand];
        }
        else if (direction == Direction.Right && variants.rightRooms != null && variants.rightRooms.Length > 0)
        {
            rand = Random.Range(0, variants.rightRooms.Length);
            roomObj = variants.rightRooms[rand];
        }
        else if (direction == Direction.Left && variants.leftRooms != null && variants.leftRooms.Length > 0)
        {
            rand = Random.Range(0, variants.leftRooms.Length);
            roomObj = variants.leftRooms[rand];
        }
        else if (direction == Direction.Main)
        {
            roomObj = variants.MainRoom;
        }

        if (roomObj != null)
        {
            GameObject existingRoomOrCorridor = CheckForExistingRoomOrCorridor(transform.position, checkRadius);

            if (existingRoomOrCorridor == null)
            {
                Instantiate(roomObj, transform.position, transform.rotation);
                roomCount++;
            }
            else
            {
                Destroy(gameObject); //Уничтожаем только новый спавнер
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private GameObject CheckForExistingRoomOrCorridor(Vector3 position, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(position, radius);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != gameObject && (collider.gameObject.CompareTag("Room") || collider.gameObject.CompareTag("Corridor")))
            {
                return collider.gameObject;
            }
        }
        return null;
    }


    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("RoomPoint")) || other.CompareTag("TriggerRoom"))
        {
            Destroy(gameObject);
        }
    }
}
