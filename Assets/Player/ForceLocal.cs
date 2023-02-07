using Unity.Mathematics;
using UnityEngine;

public class ForceLocal : MonoBehaviour
{
    [SerializeField] private float angle;
    [SerializeField] private float angleSpeed;
    private Transform _containerTransform;

    // Start is called before the first frame update
    private void Start()
    {
        angle = 0f;
        angleSpeed = 0.3f;
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
        if (Input.GetKey("a")) angle += angleSpeed;
        if (Input.GetKey("d")) angle -= angleSpeed;
    }

    private void UpdatePosition()
    {
        Vector2 position = _containerTransform.position;
        position.x += math.cos(angle);
        position.y += math.sin(angle);
        transform.position = position;
    }
}