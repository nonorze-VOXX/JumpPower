using UnityEngine;

namespace Player.Camera
{
    public enum PlayerWhere
    {
        Left = -1,
        Right = 1,
        Up = 1,
        Down = -1
    }

    public enum CameraMode
    {
        X,
        Y
    }

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

        public void GenSwitch()
        {
            GenSwitchX();
            GenSwitchY();
        }

        private void GenSwitchX()
        {
            for (var i = 0; i < 5; i++)
            for (var j = 1; j < 5; j++)
            {
                var tmpLocal = new Vector3(cameraData.cameraLocalSpace.x * j + cameraData.switchWidth,
                    (float)(cameraData.cameraLocalSpace.y * (i + 0.5)), 0);
                var newSwitch = Instantiate(cameraSwitch, tmpLocal, Quaternion.identity, transform);
                newSwitch.GetComponent<CameraSwitch>().SetMode(CameraMode.X);
            }
        }

        private void GenSwitchY()
        {
            for (var i = 1; i < 5; i++)
            for (var j = 0; j < 5; j++)
            {
                var tmpLocal = new Vector3((float)(cameraData.cameraLocalSpace.x * (j + 0.5)),
                    cameraData.cameraLocalSpace.y * i + cameraData.switchWidth, 0);
                var newSwitch = Instantiate(cameraSwitch, tmpLocal, Quaternion.Euler(0, 0, 90), transform);
                newSwitch.GetComponent<CameraSwitch>().SetMode(CameraMode.Y);
            }
        }
    }
}