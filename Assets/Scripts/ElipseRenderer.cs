using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ElipseRenderer : MonoBehaviour
{
    LineRenderer lr;

    [Range(3, 36)]
    public int segments;
    public Elipse elipse;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        CalculateElipse();
    }

    void CalculateElipse()
    {
        Vector3[] points = new Vector3[segments + 1];

        for (int i = 0; i < segments; i++)
        {
            Vector2 position2D = elipse.Evaluate((float)i / (float)segments);

            points[i] = new Vector3(position2D.x, 0f, position2D.y);
        }
        points[segments] = points[0];

        lr.positionCount = segments + 1;
        lr.SetPositions(points);
    }

    private void OnValidate()
    {
        if (Application.isPlaying && lr!=null)
            CalculateElipse();
    }
}
