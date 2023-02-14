using Player.Camera;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public CameraData cameraData;
    private PlayerWhere _from;

    private CameraMode _mode;
    private PlayerWhere _to;

    private void Start()
    {
        _mode = CameraMode.X;
    }

    private void Update()
    {
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger");
        switch (_mode)
        {
            case CameraMode.X:
                if (other.transform.position.x < transform.position.x)
                    _from = PlayerWhere.Left;
                else
                    _from = PlayerWhere.Right;
                break;
            case CameraMode.Y:
                if (other.transform.position.y < transform.position.y)
                    _from = PlayerWhere.Down;
                else
                    _from = PlayerWhere.Up;
                break;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        switch (_mode)
        {
            case CameraMode.X:
                if (other.transform.position.x > transform.position.x)
                    _to = PlayerWhere.Right;
                else
                    _to = PlayerWhere.Left;
                if (_from != _to)
                    switch (_to)
                    {
                        case PlayerWhere.Left:
                            cameraData.nowCameraLocal -= 1;
                            break;
                        case PlayerWhere.Right:
                            cameraData.nowCameraLocal += 1;
                            break;
                    }

                break;
            case CameraMode.Y:
                if (other.transform.position.y > transform.position.y)
                    _to = PlayerWhere.Up;
                else
                    _to = PlayerWhere.Down;
                if (_from != _to)
                    switch (_to)
                    {
                        case PlayerWhere.Up:
                            cameraData.nowCameraLocal += 5;
                            break;
                        case PlayerWhere.Down:
                            cameraData.nowCameraLocal -= 5;
                            break;
                    }

                break;
        }
    }

    public void SetMode(CameraMode mode)
    {
        _mode = mode;
    }
}