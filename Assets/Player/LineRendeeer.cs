using System.Collections.Generic;
using UnityEngine;

public class LineRendeeer : MonoBehaviour

{
    public LineRenderer line;

    public int count;

    public float timer;

    [SerializeField] public List<Vector3> points;

    private Vector3 _nowPosition;

    // Start is called before the first frame update
    private void Start()
    {
        count = 0;
        timer = 0;
        line = GetComponent<LineRenderer>();
        points = new List<Vector3>();
    }

    // Update is called once per frame
    private void Update()
    {
        GetInput();
        timer += Time.deltaTime;
        if (timer > 0.02)
        {
            if (_nowPosition != transform.position || points.Count == 0) AddPoints();

            _nowPosition = transform.position;
            timer = 0;
        }
    }

    private void AddPoints()
    {
        points.Add(transform.position);
        line.SetPositions(points.ToArray());
        line.positionCount = points.Count;
        line.SetPosition(line.positionCount - 1, transform.position);
    }

    private void GetInput()
    {
        if (Input.GetKey(KeyCode.Delete))
        {
            Debug.Log("DELETE!");
            points.Clear();
        }
    }
}