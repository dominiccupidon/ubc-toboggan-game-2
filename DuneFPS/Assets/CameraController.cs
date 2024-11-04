using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WiimoteApi;

public class CameraController : MonoBehaviour
{
    public Transform head;
    public float headInitY = 0.4f;
    public float cameraSensitivity = 10f;
    public float force = 5f;
    public float gravity = 9.81f;
    public bool invertY = true;
    Wiimote remote;
    Rigidbody rb;
    Collider cl;
    bool isWiimoteConnected = false;
    float multiplier = 1f;
    float headInitX = 0f;
    float headInitZ = 0f;
    float rotX = 0f;
    float rotY = 0f;
    float deltaX = 0f;
    float deltaY = 0f;
    float deltaZ = 0f;
    bool canCrouch = true;
    bool isCrouched = false;
    bool isRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cl = GetComponent<Collider>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        WiimoteManager.FindWiimotes();
        remote = WiimoteManager.Wiimotes[0]; 
        if (remote != null)
        {
            isWiimoteConnected = true;
            remote.SendDataReportMode(InputDataType.REPORT_BUTTONS); // Sends only information of button presses from the remote
        }
    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
        AcceptInput();
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
        rotY = Mathf.Clamp(rotY, -45, 45);
        head.localEulerAngles = new Vector3(rotY, 0, 0);
        transform.Rotate(rotX * Vector3.up);
    }

    void AcceptInput()
    {
        float headY = headInitY;
        multiplier = 1f;
        deltaX = 0f;
        deltaY = 0f;
        deltaZ = 0f;

        if (!isWiimoteConnected)
        {
            // This may cause playing on keyboard to behave differently from playing with the Wiimote
            deltaX = Mathf.Abs(Input.GetAxis("Horizontal")) > 0.3 ? Input.GetAxis("Horizontal") : 0f;
            deltaZ = Mathf.Abs(Input.GetAxis("Vertical")) > 0.3 ? Input.GetAxis("Vertical") : 0f;
            isRunning = Input.GetKey("space");
            if (Input.GetKeyDown("c")) {
                isCrouched = !isCrouched;
            }
        } else
        {
            int ret;
            do {
                ret = remote.ReadWiimoteData();
            } while (ret > 0);

            if (remote.Button.d_up) {
                deltaZ = 1;
            }

            if (remote.Button.d_down) {
                deltaZ = -1;
            }

            if (remote.Button.d_right) {
                deltaX = 1;
            }

            if (remote.Button.d_left) {
                deltaX = -1;
            }

            isRunning = remote.Button.b; // Requires users to hold the B button to run

            // Requires users to press the A button to toggle crouching
            //
            // Update() may be called several times before the Wii remote sends new data.
            // This means that between updates from the remote Unity will reuse stale data. 
            // For example, a single press of A will be interpreted as several presses. Since the
            // A button is a toggle for crouching, a single press of A will cause the player to 
            // crouch repeatedly for a few frames. To prevent this canCrouch acts as a buffer
            // to prevent crouching until we are confident that new data has come in from the
            // remote. We know new data has come in when remote.Button.a changes from true to false,
            // so we notify Unity that crouching can be toggled again once this occurs.
            if (remote.Button.a && canCrouch) {
                canCrouch = false;
                isCrouched = !isCrouched;
            } 

            if (!remote.Button.a) {
                canCrouch = true;
            }
        }

        multiplier = isRunning ? 2 : 1;
        multiplier = isCrouched ? 0.5f : multiplier;
        headY = isCrouched ? -0.75f : headInitY;
        head.localPosition = new Vector3(headInitX, headY, headInitZ);
    }

    void MoveAround()
    {
        Vector3 direction = new Vector3(deltaX, deltaY, deltaZ);
        direction = transform.TransformDirection(direction);
        rb.AddForce(direction * force * multiplier, ForceMode.Impulse);
    }

    void OnApplicationQuit()
    {
        // This should be uncommented for the final game
        // WiimoteManager.Cleanup(remote);
    }
}
