using UnityEngine;

namespace Player.Camera
{
    public class CameraManager : MonoBehaviour
    {
        public CameraData data;

        private void Update()
        {
            transform.position = data.cameraPosition;
        }
    }
}