using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedcapScript : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] public float maxFallSpeed = -10f;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        CapFallSpeed();
    }
    private void CapFallSpeed()
    {
        Vector3 vel = rb.velocity;

        // If falling faster than maxFallSpeed (remember: more negative means faster down)
        if (vel.y < maxFallSpeed)
        {
            vel.y = maxFallSpeed;
            rb.velocity = vel;
        }
    }
}
