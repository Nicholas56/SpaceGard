using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerShip : ShipMovement
{
    public CameraFollow follow;
    public GameEvent gameOver;

    private void FixedUpdate()
    {
        Quaternion deltaRotation =
        Quaternion.Euler(new Vector3(0, CrossPlatformInputManager.GetAxis("Horizontal")) * Time.deltaTime * 40);

        ChangeSpeed(CrossPlatformInputManager.GetAxis("Vertical"));

        rb.MoveRotation(rb.rotation * deltaRotation);
        rb.velocity = transform.forward * speed;

        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            FireMissile();
        }
        if (CrossPlatformInputManager.GetButtonDown("Fire2"))
        {
            AlignCamera();
        }
    }

    void ChangeSpeed(float deltaSpeed)
    {
        speed += deltaSpeed;
        speed = Mathf.Clamp(speed, 0, 30);
    }
    void AlignCamera()
    {
        follow.Align();
    }

    override public void Death()
    {
        base.Death();
        gameOver.Raise();
        gameObject.SetActive(false);
        FindObjectOfType<ScoreManager>().gs = ScoreManager.gameState.enterscore;
    }
}
