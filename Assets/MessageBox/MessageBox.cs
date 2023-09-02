using Steamworks;
using UnityEngine;

public class MessageBox : MonoBehaviour
{
    public GameObject triggerObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggerObject.name == "HiddenWall")
        {
            triggerObject.SetActive(false);
            SteamUserStats.SetAchievement("Dream");
            SteamUserStats.StoreStats();
            Destroy(this);
        }
        else
        {
            triggerObject.SetActive(true);
        }

        if (triggerObject.name == "firework") GameObject.Find("power").GetComponent<player_ani>().issEnd();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (triggerObject.name != "firework") triggerObject.SetActive(false);
    }
}