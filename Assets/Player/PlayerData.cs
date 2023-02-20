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
        public float hitDownDistance;

        public float collideWallSpeedDelta;

        public float baseSpeed;

        public Vector2 gravityDirection;
        public float angleLockA;
        public float angleLockD;
        public float angleLockNull;
        public float timeForce;
        public float powerTime;
        public float angle;
        public float nowGravityAngleLockA;
        public float nowGravityAngleLockD;
        public float nowGravityAngleLockNull;
        public float maxSpeed;
    }
}