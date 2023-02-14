using UnityEngine;

namespace Player.Camera
{
    public class CameraManager : MonoBehaviour
    {
        public CameraData data;

        private void Update()
        {
            var newLocal = data.cameraLocalList[data.nowCameraLocal];
            transform.position = new Vector3(newLocal.x, newLocal.y, -10);
        }
    }
}