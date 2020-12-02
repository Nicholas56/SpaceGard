using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipEffects : MonoBehaviour
{

    public Transform target;
    ParticleSystem ps;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public void PlayDeath()
    {
        transform.position = target.position;
        ps.Play();
    }

}
