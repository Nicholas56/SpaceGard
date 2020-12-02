using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ShipMovement : MonoBehaviour
{
    protected Rigidbody rb;
    [SerializeField]protected float speed = 5;
    public ShipEffects effects;

    public ObjectPool pool;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (pool == null) { pool = FindObjectOfType<ObjectPool>(); }
    }


    protected void FireMissile()
    {
        GameObject nMissile = pool.GetObject("Missile");
        nMissile.transform.position = transform.position + transform.forward *2;
        nMissile.transform.rotation =  transform.rotation;
        nMissile.GetComponent<Rigidbody>().AddForce(transform.forward * 25, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Death();
    }

    virtual public void Death()
    {
        effects.PlayDeath();
    }
}
