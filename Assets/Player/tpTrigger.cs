using UnityEngine;

public class tpTrigger : MonoBehaviour
{
    public GameObject Tpflag;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Tpflag.SetActive(true);
        Tpflag.transform.position = transform.position;
        Save();
    }

    private void Save()
    {
        var position = transform.position;
        PlayerPrefs.SetFloat("savePointX", position.x);
        PlayerPrefs.SetFloat("savePointY", position.y);
        PlayerPrefs.Save();
        // PlayerPrefs.DeleteAll();
    }
}