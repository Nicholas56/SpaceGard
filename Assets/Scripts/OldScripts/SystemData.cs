using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemData
{
    //Planetary Information
    public int planetNum;
    public List<Elipse> elipse;
    public List<float> orbProgress;
    public List<float> orbPeriod;
    public List<bool> orbActive;

    public List<Vector3> objPos = new List<Vector3>();
    public List<Quaternion> objRot = new List<Quaternion>();
    public List<int> materialIndex = new List<int>();
    public List<Vector3> objScale = new List<Vector3>();
    public List<float> objMass = new List<float>();
    public List<Vector3> objVelocity = new List<Vector3>();
}
