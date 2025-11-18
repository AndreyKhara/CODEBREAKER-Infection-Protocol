using UnityEngine;

public class RoomsVariants : MonoBehaviour
{
    public GameObject[] ForwardRooms;
    public GameObject[] BackRooms;
    public GameObject[] RightRooms;
    public GameObject[] LeftRooms;
    public GameObject[] MainRoom;

    [Header("Rooms Without Exits")]
        public GameObject[] ForwardRoomsNoForward; // Комнаты, у которых нет выхода вперед
        public GameObject[] BackRoomsNoBack; // Комнаты, у которых нет выхода назад
        public GameObject[] RightRoomsNoRight; // Комнаты, у которых нет выхода вправо
        public GameObject[] LeftRoomsNoLeft; // Комнаты, у которых нет выхода влево
}
