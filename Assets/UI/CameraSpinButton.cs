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
        v.SetActive(cameraData.isCameraSpin);
    }

    private void TaskOnClick()
    {
        cameraData.isCameraSpin = !cameraData.isCameraSpin;
        v.SetActive(cameraData.isCameraSpin);
    }
}