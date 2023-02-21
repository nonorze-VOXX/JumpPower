using UnityEngine;

public class TpTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerPrefs.SetInt("haveTP", 1);
    }
}