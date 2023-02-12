using UnityEngine;

namespace Player
{
    public struct CharaSize
    {
        public float Up;
        public float Down;
        public float Left;
        public float Right;
    }

    [CreateAssetMenu(fileName = "Data", menuName = "data/player", order = 1)]
    public class PlayerData : ScriptableObject
    {
        public float angleMax;
        public float angleMin;

        public float gravity;

        public float hitDistance;

        public float collideWallSpeedDelta;

        public float baseSpeed;
        // public CharaSize CharaSize;
    }
}