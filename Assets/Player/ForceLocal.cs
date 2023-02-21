using Player;
using Unity.Mathematics;
using UnityEngine;

public class ForceLocal : MonoBehaviour
{
    [SerializeField] private float angleSpeed;
    public PlayerData playerData;
    private Transform _containerTransform;
    private float _gravityAngleMax;
    private float _gravityAngleMin;

    private void Start()
    {
        playerData.freeAngle = false;
        playerData.angle = 0f;
        angleSpeed = 10f;
        _containerTransform = transform.parent;
        playerData.angleLockA = playerData.nowGravityAngleLockA = -60;
        playerData.angleLockD = playerData.nowGravityAngleLockD = -120;
        playerData.angleLockNull = playerData.nowGravityAngleLockNull = -90;
        _gravityAngleMax = playerData.angleMax;
        _gravityAngleMin = playerData.angleMin;
    }

    private void Update()
    {
        UpdatePosition();
        CheckAngle();
        ChangeAngle();
        PowerTimeSize();
    }

    private void PowerTimeSize()
    {
        transform.localScale = new Vector3(1, 1, 1) * ((playerData.powerTime + 0.6f) / 2);
    }


    private void CheckAngle()
    {
        if (playerData.gravityDirection.Equals(Vector2.down) &&
            !playerData.nowGravityAngleLockNull.Equals(playerData.angleLockNull))
        {
            _gravityAngleMax = playerData.angleMax;
            _gravityAngleMin = playerData.angleMin;
            _containerTransform.rotation = Quaternion.Euler(0, 0, 0);
            playerData.nowGravityAngleLockA = playerData.angleLockA;
            playerData.nowGravityAngleLockD = playerData.angleLockD;
            playerData.nowGravityAngleLockNull = playerData.angleLockNull;
        }
        else if (playerData.gravityDirection.Equals(Vector2.right) &&
                 !playerData.nowGravityAngleLockNull.Equals(Spin90Angle(playerData.angleLockNull)))
        {
            _gravityAngleMax = Spin90Angle(playerData.angleMax);
            _gravityAngleMin = Spin90Angle(playerData.angleMin);
            _containerTransform.rotation = Quaternion.Euler(0, 0, 90);
            playerData.nowGravityAngleLockA = Spin90Angle(playerData.angleLockA);
            playerData.nowGravityAngleLockD = Spin90Angle(playerData.angleLockD);
            playerData.nowGravityAngleLockNull = Spin90Angle(playerData.angleLockNull);
        }
        else if (playerData.gravityDirection.Equals(Vector2.up) &&
                 !playerData.nowGravityAngleLockNull.Equals(Spin90Angle(Spin90Angle(playerData.angleLockNull))))
        {
            _gravityAngleMax = Spin90Angle(Spin90Angle(playerData.angleMax));
            _gravityAngleMin = Spin90Angle(Spin90Angle(playerData.angleMin));
            _containerTransform.rotation = Quaternion.Euler(0, 0, 180);
            playerData.nowGravityAngleLockA = Spin90Angle(Spin90Angle(playerData.angleLockA));
            playerData.nowGravityAngleLockD = Spin90Angle(Spin90Angle(playerData.angleLockD));
            playerData.nowGravityAngleLockNull = Spin90Angle(Spin90Angle(playerData.angleLockNull));
        }
        else if (playerData.gravityDirection.Equals(Vector2.left) &&
                 !playerData.nowGravityAngleLockNull.Equals(
                     Spin90Angle(Spin90Angle(Spin90Angle(playerData.angleLockNull)))))
        {
            _gravityAngleMax = Spin90Angle(Spin90Angle(Spin90Angle(playerData.angleMax)));
            _gravityAngleMin = Spin90Angle(Spin90Angle(Spin90Angle(playerData.angleMin)));
            _containerTransform.rotation = Quaternion.Euler(0, 0, 270);
            playerData.nowGravityAngleLockA = Spin90Angle(Spin90Angle(Spin90Angle(playerData.angleLockA)));
            playerData.nowGravityAngleLockD = Spin90Angle(Spin90Angle(Spin90Angle(playerData.angleLockD)));
            playerData.nowGravityAngleLockNull = Spin90Angle(Spin90Angle(Spin90Angle(playerData.angleLockNull)));
        }
    }

    private float Spin90Angle(float beforeAngle)
    {
        return beforeAngle + 90;
    }


    private void ChangeAngle()
    {
        if (Input.GetKey(KeyCode.LeftShift)) playerData.freeAngle = !playerData.freeAngle;

        if (playerData.freeAngle)
        {
            if (Input.GetKey("a") && playerData.angle < _gravityAngleMax) playerData.angle += angleSpeed;
            if (Input.GetKey("d") && playerData.angle > _gravityAngleMin) playerData.angle -= angleSpeed;
        }
        else
        {
            if (Input.GetKey("a"))
                playerData.angle = playerData.nowGravityAngleLockA;
            else if (Input.GetKey("d"))
                playerData.angle = playerData.nowGravityAngleLockD;
            else
                playerData.angle = playerData.nowGravityAngleLockNull;
        }
    }

    private void UpdatePosition()
    {
        Vector2 position = _containerTransform.position;
        position.x += math.cos(playerData.angle * math.PI / 180);
        position.y += math.sin(playerData.angle * math.PI / 180);
        transform.position = position;
    }
}