using Player.Camera;
using UnityEngine;

namespace Player.Gravity
{
    public class GravityCreater : MonoBehaviour
    {
        public GameObject gravitySwitch;
        public GravityData data;
        public CameraData mapData;

        private void Start()
        {
            data.gravitySwitchLocal.Clear();
            data.gravitySwitchLocal.Add(new Vector2(mapData.cameraLocalSpace.x * 5 / 2,
                mapData.cameraLocalSpace.y * 0));
            data.gravitySwitchLocal.Add(new Vector2(mapData.cameraLocalSpace.x * 5,
                mapData.cameraLocalSpace.y * 5 / 2));
            data.gravitySwitchLocal.Add(new Vector2(mapData.cameraLocalSpace.x * 5 / 2,
                mapData.cameraLocalSpace.y * 5));
            data.gravitySwitchLocal.Add(new Vector2(mapData.cameraLocalSpace.x * 0,
                mapData.cameraLocalSpace.y * 5 / 2));
            var newSwitch1 = Instantiate(gravitySwitch, data.gravitySwitchLocal[0], Quaternion.identity, transform);
            newSwitch1.GetComponent<GravitySwitch>().SetMode(GravityMode.Down);
            var newSwitch2 = Instantiate(gravitySwitch, data.gravitySwitchLocal[1], Quaternion.Euler(0, 0, 90),
                transform);
            newSwitch2.GetComponent<GravitySwitch>().SetMode(GravityMode.Right);
            var newSwitch3 = Instantiate(gravitySwitch, data.gravitySwitchLocal[2], Quaternion.Euler(0, 0, 180),
                transform);
            newSwitch3.GetComponent<GravitySwitch>().SetMode(GravityMode.Up);
            var newSwitch4 = Instantiate(gravitySwitch, data.gravitySwitchLocal[3], Quaternion.Euler(0, 0, 270),
                transform);
            newSwitch4.GetComponent<GravitySwitch>().SetMode(GravityMode.Left);
        }
    }
}