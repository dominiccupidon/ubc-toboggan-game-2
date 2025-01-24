using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 5.0f;
    private float horizontalInput;
    private float forwardInput;
    private float jumpStrength = 10f;
    private Rigidbody rb;
    private bool onGround = true;
    public Transform orientation;
    Vector3 moveDirection;
    public LayerMask ground;
    public float drag = 5.0f;
    public float maxSpeed = 7.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // Move the player based on local axes but ignore vertical movement
        Vector3 forward = orientation.forward;
        forward.y = 0; // Ignore vertical component
        forward.Normalize();

        Vector3 right = orientation.right;
        right.y = 0; // Ignore vertical component
        right.Normalize();

        moveDirection = forward * forwardInput + right * horizontalInput;
        rb.AddForce(moveDirection.normalized * speed, ForceMode.Force);

        // Limit max speed
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
            onGround = false;
        }

        // Apply drag when on the ground
        if (onGround)
        {
            rb.drag = drag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }
}
