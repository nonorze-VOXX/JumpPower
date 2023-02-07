using UnityEngine;

public class PlayerPower : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform ForceLocal;

    private void Start()
    {
        ForceLocal = transform.GetChild(1);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey("w")) transform.position = ForceLocal.position;
    }
}