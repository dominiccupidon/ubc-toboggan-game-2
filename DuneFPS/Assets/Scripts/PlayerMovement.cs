using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 5.0f;
    private float horizontalInput;
    private float forwardInput;
    private float jumpStrength = 5.0f;
    private Rigidbody rb;
    private bool onGround = true;
    public Transform orientation;
    Vector3 moveDirection;
    public LayerMask ground;
    public float drag = 5.0f;
    public float maxSpeed = 7.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // Move the player based on local axes (relative to their facing direction)
        moveDirection = orientation.forward * forwardInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * speed, ForceMode.Force);

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
            onGround = false;
        }

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
