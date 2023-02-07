using Unity.Mathematics;
using UnityEngine;

public class ForceLocal : MonoBehaviour
{
    private float angle;

    // Start is called before the first frame update
    private void Start()
    {
        angle = 0f;
    }

    // Update is called once per frame
    private void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        Vector2 position = transform.position;
        position.x = math.cos(angle);
        position.y = math.sin(angle);
        transform.position = position;
    }
}