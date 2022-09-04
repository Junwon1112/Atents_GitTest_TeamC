using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleLine : MonoBehaviour
{

    LineRenderer line;

    public List<Vector3> vertices = new List<Vector3>();
    public float radius = 5.0f;


    private void Start()
    {
        line = GetComponent<LineRenderer>();
        line.useWorldSpace = false;
        CirclePoint();
  

    }

    public void CirclePoint()
    {
        float heading;
        line = GetComponent<LineRenderer>();
        vertices.Clear();
        for (int a = 0; a <= 360; a += 360 / 30)
        {
            heading = a * Mathf.Deg2Rad;
            vertices.Add(new Vector3(Mathf.Cos(heading) * radius, Mathf.Sin(heading) * radius,0.0f));
        }
        line.positionCount = vertices.Count;
        for (int i=0; i<vertices.Count; i++)
        {
            
            line.SetPosition(i, vertices[i]);
        }
    }

}
