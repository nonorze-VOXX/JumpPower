using UnityEngine;

namespace Player.Camera
{
    public class CameraSwitchManager : MonoBehaviour
    {
        public CameraData cameraData;

        public GameObject cameraSwitch;

        private void Start()
        {
            cameraData.nowCameraLocal = 0;
            cameraData.cameraLocalList.Clear();
            for (var i = 0; i < 5; i++)
            for (var j = 0; j < 5; j++)
            {
                var tmpLocal = new Vector2((float)(cameraData.cameraLocalSpace.x * (j + 0.5)),
                    (float)(cameraData.cameraLocalSpace.y * (i + 0.5)));
                cameraData.cameraLocalList.Add(tmpLocal);
            }

            GenSwitch();
        }

        private void GenSwitch()
        {
            GenOtherCase();
        }

        private void GenOtherCase()
        {
            for (var i = 0; i < 25; i++)
            {
                var newSwitch = Instantiate(cameraSwitch, cameraData.cameraLocalList[i], Quaternion.identity,
                    transform);
            }
        }
    }
}