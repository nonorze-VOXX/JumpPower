using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Camera
{
    internal struct GravityToAngle
    {
        public Vector2 Dir;
        public float Angle;
    }

    public class CameraManager : MonoBehaviour
    {
        public CameraData cameraData;
        public PlayerData playerData;
        public GameObject player;
        private readonly List<GravityToAngle> _gravityToAngle = new();
        private Vector2 _pastGravity;
        private bool _spining;

        private void Start()
        {
            cameraData.CameraStatus = CameraStatus.Normal;
            _pastGravity = playerData.gravityDirection;
            GravityToAngle gravityToAngle;
            gravityToAngle.Angle = 0;
            gravityToAngle.Dir = Vector2.down;
            _gravityToAngle.Add(gravityToAngle);
            gravityToAngle.Angle = 90;
            gravityToAngle.Dir = Vector2.right;
            _gravityToAngle.Add(gravityToAngle);
            gravityToAngle.Angle = 180;
            gravityToAngle.Dir = Vector2.up;
            _gravityToAngle.Add(gravityToAngle);
            gravityToAngle.Angle = 270;
            gravityToAngle.Dir = Vector2.left;
            _gravityToAngle.Add(gravityToAngle);
            _spining = false;
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
            foreach (var gta in _gravityToAngle)
                if (gta.Dir == playerData.gravityDirection)
                {
                    if (Math.Abs(gta.Angle % 360 - transform.rotation.eulerAngles.z % 360) > 0.1)
                    {
                        var rotation = transform.rotation;
                        rotation = Quaternion.Euler(0, 0, rotation.eulerAngles.z + 1);
                        transform.rotation = rotation;
                    }
                    else
                    {
                        _spining = false;
                        player.GetComponent<PlayerPower>().Pause();
                    }
                }
        }

        private void SpinCameraTrigger()
        {
            if (playerData.gravityDirection != _pastGravity)
            {
                _spining = true;
                player.GetComponent<PlayerPower>().Pause();
            }

            _pastGravity = playerData.gravityDirection;
        }
    }
}