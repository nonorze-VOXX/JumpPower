using System.Collections.Generic;
using UnityEngine;

namespace Player.Camera
{
    public enum CameraStatus
    {
        GameEnd,
        Normal
    }

    [CreateAssetMenu(fileName = "cameraData", menuName = "data/CameraData", order = 2)]
    public class CameraData : ScriptableObject
    {
        public Vector2 cameraLocalSpace;
        public List<Vector2> cameraLocalList;
        public Vector3 cameraPosition;
        public bool isCameraSpin;
        public CameraStatus CameraStatus;
        public float spinSpeed;
    }
}