using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_control : MonoBehaviour
{
    Rigidbody rb;
    public float speed;
    public float sensitivity;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0,  Input.GetAxisRaw("Vertical"));
        Vector3 mouseLook = new Vector3(Input.GetAxisRaw("Mouse Y"), Input.GetAxisRaw("Mouse X"), 0) * sensitivity;
        Vector3 relativeMot = transform.TransformDirection(input.normalized) * speed;
        relativeMot.y = 0;
        rb.velocity = relativeMot;
        transform.Rotate(Vector3.up * mouseLook.y);
        transform.Rotate(Vector3.right *  -mouseLook.x, Space.Self);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y);
    }
}
