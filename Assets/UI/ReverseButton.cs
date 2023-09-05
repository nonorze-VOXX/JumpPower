using Player;
using UnityEngine;
using UnityEngine.UI;

public class ReverseButton : MonoBehaviour
{
    public Button yourButton;
    public GameObject v;
    public PlayerData playerData;

    private void Start()
    {
        v.SetActive(playerData.controlReverse);
    }

    public void PlayerRightLeftReverse()
    {
        playerData.controlReverse = !playerData.controlReverse;
        v.SetActive(playerData.controlReverse);
    }
}