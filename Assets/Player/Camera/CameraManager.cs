using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Camera
{
    public class CameraManager : MonoBehaviour
    {
        public CameraData cameraData;
        public PlayerData playerData;
        public GameObject player;
        private Vector2 _pastGravity;
        private bool _spining;
        private Dictionary<Vector2, float> gravityToDir;
        private Vector3 spinDirection;

        private void Start()
        {
            cameraData.CameraStatus = CameraStatus.Normal;
            gravityToDir = new Dictionary<Vector2, float>();
            gravityToDir.Add(Vector2.down, 0);
            gravityToDir.Add(Vector2.up, 180);
            gravityToDir.Add(Vector2.right, 90);
            gravityToDir.Add(Vector2.left, 270);
            _pastGravity = playerData.gravityDirection;
            _spining = false;
            if (cameraData.isCameraSpin)
                transform.rotation = Quaternion.Euler(0, 0, gravityToDir[playerData.gravityDirection]);
        }

        private void Update()
        {
            switch (cameraData.CameraStatus)
            {
                case CameraStatus.Normal:
                    transform.position = cameraData.cameraPosition;
                    if (cameraData.isCameraSpin)
                    {
                        if (_spining)
                            SpinCamera();
                        else
                            SpinCameraTrigger();
                    }

                    break;
                case CameraStatus.GameEnd:

                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    var newPosition = GameObject.Find("power").transform.position;
                    newPosition.z = -10;
                    transform.position = newPosition;
                    break;
            }
        }

        private void SpinCamera()
        {
            var gta = gravityToDir[playerData.gravityDirection];
            if (Math.Abs(gta % 360 - transform.rotation.eulerAngles.z % 360) > 0.1)
            {
                var rotation = transform.rotation;
                rotation = Quaternion.Euler(0, 0, rotation.eulerAngles.z + spinDirection.z);
                transform.rotation = rotation;
            }
            else
            {
                _pastGravity = playerData.gravityDirection;
                _spining = false;
                player.GetComponent<PlayerPower>().Pause();
            }
        }

        private void SpinCameraTrigger()
        {
            if (playerData.gravityDirection != _pastGravity)
            {
                _spining = true;
                spinDirection = Vector3.Cross(_pastGravity, playerData.gravityDirection);

                // if player from left Gravity to right Gravity need this if
                if (spinDirection.z == 0) spinDirection.z = 1;
                player.GetComponent<PlayerPower>().Pause();
            }
        }
    }
}