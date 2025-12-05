using UnityEngine;

[RequireComponent(typeof(Collider))]
public class RoomLogic : MonoBehaviour
{
    private MiniMapRoom miniMapRoom;

    private void Awake()
    {
        miniMapRoom = GetComponent<MiniMapRoom>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (miniMapRoom != null)
            {
                MiniMap.Instance.PlayerEnteredRoom(miniMapRoom);
            }
            else
            {
                Debug.LogError("Не могу открыть комнату: ссылка на MiniMapRoom отсутствует!");
            }
        }
    }
}