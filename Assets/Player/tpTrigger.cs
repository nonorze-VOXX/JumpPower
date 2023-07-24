using Player;
using Player.save;
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
        if (PlayCase.saveCase == SaveCase.onlyTpTrigger)
        {
            var position = transform.position;
            var saver = new JumpPowerSaver();
            saver.SetSavePosition(position);
        }
    }
}