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
    
        const float rotate90degTime = 1.0f;

        private void Start()
        {
            _pastGravity = playerData.gravityDirection;
            _spining = false;
        }

        private void Update()
        {
            transform.position = cameraData.cameraPosition;
            if (cameraData.canSpin)
            {
                if (_spining) {
                    var done = SpinCamera();
                    if (done)
                        PausePlay(); // play
                }
                else
                {
                    if (playerData.gravityDirection != _pastGravity)
                        PausePlay(); // pause
                }   
                _pastGravity = playerData.gravityDirection;
            }
        }
        private void PausePlay()
        {
            _spining = !_spining;
            player.GetComponent<PlayerPower>().PausePlay();
        }

        private bool SpinCamera()
        {
            float targetAngle = Vector2.SignedAngle(Vector2.down, playerData.gravityDirection);
            if (targetAngle < 0) targetAngle += 360;
            //Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
            
            float currentAngle = transform.rotation.eulerAngles.z;
            //float angleBetween = Quaternion.Angle(transform.rotation, targetRotation);
            float angleBetween = targetAngle - currentAngle;

            float dRo = Time.deltaTime * (90.0f / rotate90degTime);

            if (1 < angleBetween)
                transform.Rotate(0, 0, dRo);
            else if (angleBetween < -1)
                transform.Rotate(0, 0, -dRo);
            else
            {
                transform.rotation = targetRotation;
                return true;
            }
            return false;
        }
    }
}