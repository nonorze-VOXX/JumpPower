using UnityEngine;

public class MessageBox : MonoBehaviour
{
    public GameObject triggerObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggerObject.name == "HiddenWall")
            triggerObject.SetActive(false);
        else
            triggerObject.SetActive(true);
        if (triggerObject.name == "firework") GameObject.Find("power").GetComponent<player_ani>().issEnd();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (triggerObject.name != "firework") triggerObject.SetActive(false);
    }
}