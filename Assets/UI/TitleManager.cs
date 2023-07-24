using Player;
using Player.save;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
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

    public void Continue()
    {
        var jumpPowerSaver = new JumpPowerSaver();
        playerData.savedPosition = jumpPowerSaver.GetSavePosition();
        SceneManager.LoadScene("JumpPower");
    }

    public void NewGame()
    {
        playerData.gravityDirection = Vector2.down;
        playerData.savedPosition = playerData.playerInitPosition;
        var jumpPowerSaver = new JumpPowerSaver();
        jumpPowerSaver.SetSavePosition(playerData.playerInitPosition);
        SceneManager.LoadScene("JumpPower");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}