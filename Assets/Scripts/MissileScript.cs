using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
    public ObjectPool pool;
    public float timeUntilDestroy;

    private void Awake()
    {
        if (pool == null) { pool = FindObjectOfType<ObjectPool>(); }
    }
    private void OnEnable()
    {
        Invoke("DestroyMissile", timeUntilDestroy);
    }

    void DestroyMissile()
    {
        if(gameObject.activeSelf)
        pool.ReturnObject(gameObject,"Missile");
    }

    private void OnCollisionEnter(Collision collision)
    {
        DestroyMissile();
    }
}
