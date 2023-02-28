using UnityEngine;

public class tpTrigger : MonoBehaviour
{
    public GameObject Tpflag;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Tpflag.SetActive(true);
        Tpflag.transform.position = transform.position;
    }
}