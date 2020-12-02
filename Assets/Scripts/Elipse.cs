using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Elipse
{
    public float xAxis;
    public float yAxis;

    public Elipse(float xAx, float yAx)
    {
        xAxis = xAx;
        yAxis = yAx;
    }

    public Vector2 Evaluate(float t)
    {
        float angle = Mathf.Deg2Rad * 360 * t;
        float x = Mathf.Sin(angle) * xAxis;
        float y = Mathf.Cos(angle) * yAxis;

        return new Vector2(x, y);
    }
}
