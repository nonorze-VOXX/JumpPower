using Player.Camera;
using Unity.Mathematics;
using UnityEngine;

namespace Player.Gravity
{
    public enum GravityMode
    {
        Left,
        Right,
        Up,
        Down
    }

    public class GravitySwitch : MonoBehaviour
    {
        public PlayerData playerData;
        public CameraData mapData;
        private GravityMode _mode;

        private void Start()
        {
            GetComponent<BoxCollider2D>().size = mapData.cameraLocalSpace * 5 / math.sqrt(2);
            transform.rotation = Quaternion.Euler(0, 0, 45);
        }


        private void OnTriggerStay2D(Collider2D other)
        {
            var playerTag = "Player";
            if (other.transform.CompareTag(playerTag))
                switch (_mode)
                {
                    case GravityMode.Down:
                        playerData.gravityDirection = Vector2.down;
                        break;
                    case GravityMode.Up:
                        playerData.gravityDirection = Vector2.up;
                        break;
                    case GravityMode.Left:
                        playerData.gravityDirection = Vector2.left;
                        break;
                    case GravityMode.Right:
                        playerData.gravityDirection = Vector2.right;
                        break;
                }
        }

        public void SetMode(GravityMode mode)
        {
            _mode = mode;
        }
    }
}