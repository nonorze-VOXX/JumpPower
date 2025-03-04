using Player.Camera;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        private readonly UnityBridge<Button> cameraSpinBtn = new("Canvas/CameraSpinButton");
        private readonly UnityBridge<Scrollbar> volumnBar = new("Canvas/VolumnBar");
        private CameraData cameraData;

        private void Awake()
        {
            cameraData = Resources.Load<CameraData>("Data/CameraData");
            var ins = cameraSpinBtn.GetInstance();
            ins.onClick.RemoveAllListeners();
            ins.onClick.AddListener(ToggleCameraSpin);

            var volume = volumnBar.GetInstance();
            volume.onValueChanged.RemoveAllListeners();
            volume.onValueChanged.AddListener(f =>
            {
                var music = new UnityBridge<AudioSource>("Map/Music/music");
                music.GetInstance().volume = f / 2;
            });
            volume.value = 0.2f;
        }

        private void Start()
        {
        }

        private void ToggleCameraSpin()
        {
            var v = new UnityBridge<GameObject>("Canvas/CameraSpinButton/v").GetInstance();
            v.SetActive(!v.activeSelf);
            cameraData.isCameraSpin = v.activeSelf;
        }
    }
}