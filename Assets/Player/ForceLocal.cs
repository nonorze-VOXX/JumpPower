using Player;
using Unity.Mathematics;
using UnityEngine;

public class ForceLocal : MonoBehaviour
{
    [SerializeField] private float angle;
    [SerializeField] private float angleSpeed;
    public PlayerData playerData;
    private Transform _containerTransform;
    private bool _freeAngle;
    private float _nowGravityAngleLockA;
    private float _nowGravityAngleLockD;
    private float _nowGravityAngleLockNull;

    private void Start()
    {
        angle = 0f;
        angleSpeed = 10f;
        _containerTransform = transform.parent;
        playerData.angleLockA = _nowGravityAngleLockA = -60;
        playerData.angleLockD = _nowGravityAngleLockD = -120;
        playerData.angleLockNull = _nowGravityAngleLockNull = -90;
    }

    private void Update()
    {
        UpdatePosition();
        CheckAngle();
        ChangeAngle();
    }

    private void CheckAngle()
    {
        if (playerData.gravityDirection.Equals(Vector2.down) &&
            !_nowGravityAngleLockNull.Equals(playerData.angleLockNull))
        {
            _nowGravityAngleLockA = playerData.angleLockA;
            _nowGravityAngleLockD = playerData.angleLockD;
            _nowGravityAngleLockNull = playerData.angleLockNull;
        }
        else if (playerData.gravityDirection.Equals(Vector2.right) &&
                 !_nowGravityAngleLockNull.Equals(Spin90Angle(playerData.angleLockNull)))
        {
            _nowGravityAngleLockA = Spin90Angle(playerData.angleLockA);
            _nowGravityAngleLockD = Spin90Angle(playerData.angleLockD);
            _nowGravityAngleLockNull = Spin90Angle(playerData.angleLockNull);
        }
        else if (playerData.gravityDirection.Equals(Vector2.up) &&
                 !_nowGravityAngleLockNull.Equals(Spin90Angle(Spin90Angle(playerData.angleLockNull))))
        {
            _nowGravityAngleLockA = Spin90Angle(Spin90Angle(playerData.angleLockA));
            _nowGravityAngleLockD = Spin90Angle(Spin90Angle(playerData.angleLockD));
            _nowGravityAngleLockNull = Spin90Angle(Spin90Angle(playerData.angleLockNull));
        }
        else if (playerData.gravityDirection.Equals(Vector2.left) &&
                 !_nowGravityAngleLockNull.Equals(Spin90Angle(Spin90Angle(Spin90Angle(playerData.angleLockNull)))))
        {
            _nowGravityAngleLockA = Spin90Angle(Spin90Angle(Spin90Angle(playerData.angleLockA)));
            _nowGravityAngleLockD = Spin90Angle(Spin90Angle(Spin90Angle(playerData.angleLockD)));
            _nowGravityAngleLockNull = Spin90Angle(Spin90Angle(Spin90Angle(playerData.angleLockNull)));
        }
    }

    private float Spin90Angle(float beforeAngle)
    {
        return beforeAngle + 90;
    }


    private void ChangeAngle()
    {
        if (Input.GetKey(KeyCode.LeftShift)) _freeAngle = !_freeAngle;

        if (_freeAngle)
        {
            if (Input.GetKey("a") && angle < playerData.angleMax) angle += angleSpeed;
            if (Input.GetKey("d") && angle > playerData.angleMin) angle -= angleSpeed;
        }
        else
        {
            if (Input.GetKey("a"))
                angle = _nowGravityAngleLockA;
            else if (Input.GetKey("d"))
                angle = _nowGravityAngleLockD;
            else
                angle = _nowGravityAngleLockNull;
        }
    }

    private void UpdatePosition()
    {
        Vector2 position = _containerTransform.position;
        position.x += math.cos(angle * math.PI / 180);
        position.y += math.sin(angle * math.PI / 180);
        transform.position = position;
    }
}