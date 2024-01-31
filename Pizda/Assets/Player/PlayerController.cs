using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Joystick joystick;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float dirX = joystick.Horizontal * speed;
        float dirZ = joystick.Vertical * speed;

        rb.velocity = new Vector3(dirX, 0, dirZ);
    }
}