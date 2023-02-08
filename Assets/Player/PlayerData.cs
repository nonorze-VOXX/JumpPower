using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "data/player", order = 1)]
public class PlayerData : ScriptableObject
{
    public float angleMax;
    public float angleMin;
}