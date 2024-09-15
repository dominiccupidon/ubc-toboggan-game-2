using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform head;
    public float headInitY = 0.4f;
    public float cameraSensitivity = 10f;
    public float speed = 12f;
    public float gravity = 9.81f;
    public bool invertY = true;
    Rigidbody rb;
    Collider cl;
    float headInitX = 0f;
    float headInitZ = 0f;
    float rotX = 0f;
    float rotY = 0f;
    float deltaX = 0f;
    float deltaY = 0f;
    float deltaZ = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cl = GetComponent<Collider>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
    }

    void FixedUpdate()
    {
        MoveAround();
        // Add artificial gravity because Unity's default settings feels floaty
        rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
    }

    void LookAround()
    {
        float sign = invertY ? -1f : 1f;
        rotY += Input.GetAxis("Mouse Y") * sign * cameraSensitivity;
        rotX = Input.GetAxis("Mouse X") * cameraSensitivity;
        rotX = Mathf.Clamp(rotX, -60, 60);
        head.localEulerAngles = new Vector3(rotY, 0, 0);
        transform.Rotate(rotX * Vector3.up);
    }

    // TODO: Add PS4 Controller Support
    void MoveAround()
    {
        bool isCrouched = false;
        deltaX = Mathf.Abs(Input.GetAxis("Horizontal")) > 0.3 ? Input.GetAxis("Horizontal") : 0f;
        deltaZ = Mathf.Abs(Input.GetAxis("Vertical")) > 0.3 ? Input.GetAxis("Vertical") : 0f;

        if (Input.GetKeyDown("space")) {
            deltaX *= 2;
            deltaZ *= 2;
        }

        if (Input.GetKeyDown("c")) {
            head.localPosition = new Vector3(headInitX, -0.75f, headInitZ);
            deltaX *= 0.5f;
            deltaZ *= 0.5f;
            isCrouched = true;
        }
        if (Input.GetKeyUp("c")) {
            head.localPosition = new Vector3(headInitX, headInitY, headInitZ);
            deltaX *= 2f;
            deltaZ *= 2f;
            isCrouched = false;
        }

        Vector3 velocity = new Vector3(deltaX, deltaY, deltaZ);
        rb.velocity = transform.TransformDirection(velocity * speed);
    }
}
