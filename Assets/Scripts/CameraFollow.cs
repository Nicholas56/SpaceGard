using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;
    float currentZoom = 1;
    public float minZoom = 0.5f;
    public float maxZoom = 3f;
    public float zoomSpeed = 1f;
    public float pitch = 3;
    public float yaw = 0;

    bool follow = true;

    private void LateUpdate()
    {
        transform.position = target.position - new Vector3(offset.x,offset.y*currentZoom*0.5f,offset.z*currentZoom);
        transform.LookAt(target.position + Vector3.up * pitch);
        if (follow) { yaw = target.eulerAngles.y; }
        transform.RotateAround(target.position, Vector3.up, yaw);
    }

    public void Align()
    {
        follow= !follow;
    }

    public void ChangeZoom(float deltaZoom)
    {
        currentZoom += deltaZoom * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
    }
}
