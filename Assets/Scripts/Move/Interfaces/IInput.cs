using UnityEngine;
namespace CDB.Input
{
    public interface IInput
    {
        Vector2 MoveAxis { get; }

        /*bool GetMoveForward { get; }
        bool GetMoveBackward { get; }
        bool GetRotateLeft { get; }
        bool GetRotateRight { get; }*/
    
    }
}