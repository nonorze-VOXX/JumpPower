using Player.Camera;
using UnityEngine;
using UnityEngine.UI;

public class CameraSpinButton : MonoBehaviour
{
    public Button yourButton;
    public GameObject v;
    public CameraData cameraData;


    private void Start()
    {
        var btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        v.SetActive(cameraData.canSpin);
    }

    private void TaskOnClick()
    {
        cameraData.canSpin = !cameraData.canSpin;
        v.SetActive(cameraData.canSpin);
    }
}