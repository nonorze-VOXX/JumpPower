using UnityEngine;

public class MessageBox : MonoBehaviour
{
    public GameObject triggerObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        triggerObject.SetActive(true);
        if (triggerObject.name == "firework") GameObject.Find("power").GetComponent<player_ani>().issEnd();
        // Invoke("turnOffFirework", 4);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (triggerObject.name != "firework") triggerObject.SetActive(false);
    }

    private void turnOffFirework()
    {
        triggerObject.SetActive(false);
    }
}