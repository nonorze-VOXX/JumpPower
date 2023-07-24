using Player;
using Player.save;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    public PlayerData playerData;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void Click()
    {
        playerData.savedPosition = playerData.playerInitPosition;
        var jumpPowerSaver = new JumpPowerSaver();
        jumpPowerSaver.SetSavePosition(playerData.playerInitPosition);
        SceneManager.LoadScene("JumpPower");
    }
}