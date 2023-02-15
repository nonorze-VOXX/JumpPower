using UnityEngine;

namespace Player.Camera
{
    public class CameraSwitch : MonoBehaviour
    {
        public CameraData cameraData;


        private void OnTriggerStay2D(Collider2D other)
        {
            cameraData.cameraPosition = new Vector3(transform.position.x, transform.position.y, -10);
        }
    }
}