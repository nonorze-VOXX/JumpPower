using UnityEngine;
using UnityEngine.SceneManagement;

public class exitGame : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("Title");
    }
}