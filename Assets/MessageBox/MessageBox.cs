using Steamworks;
using UnityEngine;

public class MessageBox : MonoBehaviour
{
    public GameObject triggerObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggerObject.name == "HiddenWall")
        {
            bool achieved;
            SteamUserStats.GetAchievement("Dream", out achieved);
            if (!achieved)
            {
                SteamUserStats.SetAchievement("Dream");
                SteamUserStats.StoreStats();
            }

            triggerObject.SetActive(false);
            Destroy(this);
        }
        else
        {
            triggerObject.SetActive(true);
        }

        if (triggerObject.name == "firework")
        {
            GameObject.Find("power").GetComponent<player_ani>().issEnd();
            bool achieved;
            SteamUserStats.GetAchievement("BreakingFuse", out achieved);
            if (!achieved)
            {
                SteamUserStats.SetAchievement("BreakingFuse");
                SteamUserStats.StoreStats();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (triggerObject.name != "firework") triggerObject.SetActive(false);
    }
}