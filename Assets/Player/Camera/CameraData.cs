using System.Collections.Generic;
using UnityEngine;

namespace Player.Camera
{
    [CreateAssetMenu(fileName = "cameraData", menuName = "data/CameraData", order = 2)]
    public class CameraData : ScriptableObject
    {
        public Vector2 cameraLocalSpace;
        public List<Vector2> cameraLocalList;
        public int nowCameraLocal;
        public float switchWidth;
    }
}