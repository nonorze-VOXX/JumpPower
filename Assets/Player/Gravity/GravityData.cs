using System.Collections.Generic;
using UnityEngine;

namespace Player.Gravity
{
    [CreateAssetMenu(fileName = "GravityData", menuName = "data/GravityData", order = 3)]
    public class GravityData : ScriptableObject
    {
        [SerializeField] public List<Vector2> gravitySwitchLocal;
    }
}