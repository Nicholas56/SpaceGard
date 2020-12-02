using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : ShipMovement
{
    public float turnSpeed = 10f;

    public bool isDodge;
    public Transform target;
    float timer;

    public GameEvent destroyed;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if(FindObjectOfType<PlayerShip>())
        target = FindObjectOfType<PlayerShip>().transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDodge)
        {
            transform.LookAt(target);
        }
        else
        {
            rb.MoveRotation( Quaternion.Euler(0, turnSpeed, 0));
        }
        rb.velocity = transform.forward * speed;
        if (Time.time > timer)
        {
            LookForward();
            PlayerCheck();
            timer = Time.time + 1f;
        }
    }

    void PlayerCheck() 
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + transform.forward, transform.forward, out hit, 20))
        {
            if (hit.transform.tag == "Player")
            {
                FireMissile();
            }
        }
    }

    public void Victory()
    {
        target = null;
        transform.position += Vector3.up * 3;
        speed = 0;
    }


    void LookForward()
    {
        RaycastHit[] hitInfo = Physics.SphereCastAll(transform.position + transform.forward, 1, transform.forward, 2);

        if (hitInfo.Length > 0)
        {
            foreach (var item in hitInfo)
            {
                if (item.transform.tag == "Planet")
                {
                    isDodge = true;
                    turnSpeed += 10f;
                }
            }
        }
        else
        {
            isDodge = false;
            turnSpeed = 10f;
        }
    }

    public override void Death()
    {
        base.Death();
        destroyed.Raise();
        pool.ReturnObject(gameObject,"Enemy");
    }
}
