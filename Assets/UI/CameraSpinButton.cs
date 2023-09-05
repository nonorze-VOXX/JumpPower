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
        v.SetActive(cameraData.isCameraSpin);
    }

    public void TaskOnClick()
    {
        cameraData.isCameraSpin = !cameraData.isCameraSpin;
        v.SetActive(cameraData.isCameraSpin);
    }
}