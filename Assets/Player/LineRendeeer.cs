using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendeeer : MonoBehaviour

{
        public  LineRenderer line;

        public int count;

        public float timer;
        public List<Vector3> points;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        timer = 0;
        line = GetComponent<LineRenderer>();
        points = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.02)
        {
        AddPoints();
        timer = 0;
        }
        
    }

    private void AddPoints()
    {
        points.Add(transform.position);
        line.SetPositions(points.ToArray());
        line.positionCount += 1;
    }
}
