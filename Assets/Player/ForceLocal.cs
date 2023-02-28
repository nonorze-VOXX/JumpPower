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
        float gAngle = 0;
        if (playerData.gravityDirection.Equals(Vector2.down)) gAngle = 0;
        if (playerData.gravityDirection.Equals(Vector2.right)) gAngle = 90;
        if (playerData.gravityDirection.Equals(Vector2.up)) gAngle = 180;
        if (playerData.gravityDirection.Equals(Vector2.left)) gAngle = 270;

        if (!playerData.nowGravityAngleLockNull.Equals(playerData.angleLockNull + gAngle))
        {
            _gravityAngleMax = playerData.angleMax + gAngle;
            _gravityAngleMin = playerData.angleMin + gAngle;
            _containerTransform.rotation = Quaternion.Euler(0, 0, gAngle);
            playerData.nowGravityAngleLockA = playerData.angleLockA + gAngle;
            playerData.nowGravityAngleLockD = playerData.angleLockD + gAngle;
            playerData.nowGravityAngleLockNull = playerData.angleLockNull + gAngle;
        }
    }

    private void ChangeAngle()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) playerData.freeAngle = !playerData.freeAngle;

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

    private Vector2 DegToVec2(float degree)
    {
        var v = new Vector2();
        var radian = degree * math.PI / 180;
        v.x = math.cos(radian);
        v.y = math.sin(radian);
        return v;
    }

    private void UpdatePosition()
    {
        Vector2 position = _containerTransform.position;
        transform.position = position + DegToVec2(playerData.angle);
    }
}