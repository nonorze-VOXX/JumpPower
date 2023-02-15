using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "Data", menuName = "data/player", order = 1)]
    public class PlayerData : ScriptableObject
    {
        public float angleMax;
        public float angleMin;

        public float gravity;

        public float hitDistance;

        public float collideWallSpeedDelta;

        public float baseSpeed;

        public Vector2 gravityDirection;
    }
}