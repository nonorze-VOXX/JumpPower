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
        private GravityManager gravityManager;

        private void Awake()
        {
            cameraData = Resources.Load<CameraData>("Data/CameraData");
            gravityManager = new GravityManager(new Vector2());
            CameraSpinButtonInit();

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

        private void CameraSpinButtonInit()
        {
            var ins = cameraSpinBtn.GetInstance();
            ins.onClick.RemoveAllListeners();
            ins.onClick.AddListener(() =>

            {
                var v = new UnityBridge<GameObject>("Canvas/CameraSpinButton/v").GetInstance();
                v.SetActive(!v.activeSelf);
                cameraData.isCameraSpin = v.activeSelf;
            });
        }
    }

    internal class GravityManager
    {
        public GravityManager(Vector2 initGravity)
        {
            SetGravity(initGravity);
        }

        public void SetGravity(Vector2 gravity)
        {
            Physics2D.gravity = gravity;
        }

        public void SetGravityDir(Vector2 dir)
        {
            var length = Physics2D.gravity.magnitude;
            Physics2D.gravity = dir.normalized * length;
        }

        public void SetGravityLength(Vector2 length)
        {
            var dir = Physics2D.gravity.normalized;
            Physics2D.gravity = dir * length.magnitude;
        }
    }
}