using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private float speed = 20.0f; // Increased speed
    private float horizontalInput;
    private float forwardInput;
    private float jumpStrength = 7.0f; // Increase jump strength if needed
    private Rigidbody rb;
    private bool onGround = true;
    public Transform orientation;
    Vector3 moveDirection;
    public LayerMask ground;
    public float drag = 2.0f;
    public float maxSpeed = 20.0f; // Increased max speed

    public float slopeLimit = 50f; // Increase slope limit to handle steeper slopes

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        // Get input
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // Calculate movement direction based on player orientation
        moveDirection = orientation.forward * forwardInput + orientation.right * horizontalInput;

        if (OnSlope())
        {
            // On slopes, apply movement along the slope direction
            Vector3 slopeDirection = Vector3.ProjectOnPlane(moveDirection, GetSlopeNormal());
            rb.velocity = new Vector3(slopeDirection.x * speed, rb.velocity.y, slopeDirection.z * speed);
        }
        else
        {
            // Move normally on flat ground
            rb.velocity = new Vector3(moveDirection.normalized.x * speed, rb.velocity.y, moveDirection.normalized.z * speed);
        }

        // Limit maximum speed on flat ground and slopes
        Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (horizontalVelocity.magnitude > maxSpeed)
        {
            rb.velocity = horizontalVelocity.normalized * maxSpeed + new Vector3(0, rb.velocity.y, 0);
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpStrength, rb.velocity.z);
            onGround = false;
        }

        // Apply drag when grounded
        rb.drag = onGround ? drag : 0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1.5f, ground))
        {
            float angle = Vector3.Angle(hit.normal, Vector3.up);
            return angle < slopeLimit && angle > 0;
        }
        return false;
    }

    private Vector3 GetSlopeNormal()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1.5f, ground))
        {
            return hit.normal;
        }
        return Vector3.up;
    }
}
