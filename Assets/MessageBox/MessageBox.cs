using UnityEngine;

public class MessageBox : MonoBehaviour
{
    public GameObject triggerObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        triggerObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        triggerObject.SetActive(false);
    }
}