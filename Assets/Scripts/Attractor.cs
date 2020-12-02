using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Using the Brackeys tutorial for simulating gravity
public class Attractor : MonoBehaviour
{
    public static List<Attractor> Attractors;
    public Rigidbody rb;
    const float G = 667.4f;

    private void FixedUpdate()
    {
        foreach(Attractor attractor in Attractors)
        {
            if (attractor != this)
                Attract(attractor);
        }
    }

    private void OnEnable()
    {
        if (Attractors == null)
            Attractors = new List<Attractor>();

        Attractors.Add(this);
    }
    private void OnDisable()
    {
        Attractors.Remove(this);
    }
    void Attract(Attractor objToAttract)
    {
        Rigidbody rbToAttract = objToAttract.rb;

        Vector3 direction = rb.position - rbToAttract.position;
        float distance = direction.magnitude;

        if (distance == 0f)
            return;

        float forceMagnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;

        rbToAttract.AddForce(force);
    }
}
