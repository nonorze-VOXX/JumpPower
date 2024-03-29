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
        private Quaternion _pastQuaternion;
        private float _spinCounter;
        private bool _spining;

        private readonly Dictionary<Vector2, float> gravityToDir = new()
        {
            { Vector2.zero, 0 }, { Vector2.down, 0 }, { Vector2.up, 180 }, { Vector2.right, 90 }, { Vector2.left, 270 }
        };

        private void Start()
        {
            cameraData.CameraStatus = CameraStatus.Normal;
            _pastQuaternion = Quaternion.Euler(0, 0, gravityToDir[playerData.gravityDirection]);
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
                var speed = cameraData.spinSpeed;
                var targetAngle = Quaternion.Euler(0, 0, gta);
                var newAngle = Quaternion.Slerp(_pastQuaternion, targetAngle, _spinCounter);
                _spinCounter += Time.deltaTime;
                if (Mathf.Abs(newAngle.eulerAngles.z - gta) < 2) newAngle = targetAngle;
                transform.rotation = newAngle;
            }
            else
            {
                _pastQuaternion = Quaternion.Euler(0, 0, gravityToDir[playerData.gravityDirection]);
                _spining = false;
                player.GetComponent<PlayerPower>().Pause();
            }
        }

        private void SpinCameraTrigger()
        {
            if (Quaternion.Euler(0, 0, gravityToDir[playerData.gravityDirection]) != _pastQuaternion)
            {
                _spining = true;
                player.GetComponent<PlayerPower>().Pause();
                _pastQuaternion = transform.rotation;
                _spinCounter = 0;
            }
        }
    }
}