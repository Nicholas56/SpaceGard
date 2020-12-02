using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitMotion : MonoBehaviour
{
    public Transform orbitingObject;
    public Elipse orbitPath;

    [Range(0f,1f)]
    public float orbitProgress = 0;
    public float orbitPeriod = 3;
    public bool orbitActive = true;

    // Start is called before the first frame update
    void Start()
    {
        SetOrbit();
    }

    public void SetOrbit()
    {
        if (orbitingObject == null)
        {
            orbitActive = false;
            return;
        }
        SetOrbitingObjectPosition();
        StartCoroutine(AnimateOrbit());
    }

    public void RandomizeOrbit(Elipse orbSpace)
    {
        Elipse randOrbit = new Elipse(Random.Range(orbSpace.xAxis, 2 * orbSpace.xAxis), Random.Range(orbSpace.yAxis, 2 * orbSpace.yAxis));
        orbitPath = randOrbit;
        orbitProgress = Random.Range(0f, 1f);
        //C = 2 x π x √((a2 + b2) ÷ 2) circumference of a ellipse
        float circ = 2*Mathf.PI * Mathf.Sqrt((Mathf.Pow(orbitPath.xAxis,2) + Mathf.Pow(orbitPath.yAxis,2))/2);
        orbitPeriod = circ/5;
    }

    void SetOrbitingObjectPosition()
    {
        Vector2 orbitPos = orbitPath.Evaluate(orbitProgress);
        orbitingObject.localPosition = new Vector3(orbitPos.x, 0, orbitPos.y);
    }

    IEnumerator AnimateOrbit()
    {
        if (orbitPeriod < 0.1f)
            orbitPeriod = 0.1f;
        float orbitSpeed = 1f / orbitPeriod;
        while (orbitActive)
        {
            orbitProgress += Time.deltaTime * orbitSpeed;
            orbitProgress %= 1f;
            SetOrbitingObjectPosition();
            yield return null;
        }
    }
}
