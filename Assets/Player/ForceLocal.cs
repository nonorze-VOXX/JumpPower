using Unity.Mathematics;
using UnityEngine;

public class ForceLocal : MonoBehaviour
{
    [SerializeField] private float angle;
    [SerializeField] private float angleSpeed;
    public PlayerData playerData;
    private Transform _containerTransform;

    // Start is called before the first frame update
    private void Start()
    {
        angle = 0f;
        angleSpeed = 10f;
        _containerTransform = transform.parent;
    }

    // Update is called once per frame
    private void Update()
    {
        UpdatePosition();
        ChangeAngle();
    }

    private void ChangeAngle()
    {
        if (Input.GetKey("a") && angle < playerData.angleMax) angle += angleSpeed;
        if (Input.GetKey("d") && angle > playerData.angleMin) angle -= angleSpeed;
    }

    private void UpdatePosition()
    {
        Vector2 position = _containerTransform.position;
        position.x += math.cos(angle * math.PI / 180);
        position.y += math.sin(angle * math.PI / 180);
        transform.position = position;
    }
}