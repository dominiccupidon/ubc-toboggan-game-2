using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WiimoteApi;

public class WiiCubeController : MonoBehaviour
{
    public Rigidbody rb;
    public float gravity = 10f;
    public float force = 5f;
    private Wiimote remote;

    // Start is called before the first frame update
    void Start()
    {
        WiimoteManager.FindWiimotes();
        remote = WiimoteManager.Wiimotes[0];    
        remote.SendDataReportMode(InputDataType.REPORT_BUTTONS); // Sends only information of button presses from the remote
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        int ret;
        // Code for polling the Wii remote
        do 
        {
            ret = remote.ReadWiimoteData();
        } while (ret > 0);

        float dX = 0f;
        float dY = 0f;
        float dZ = 0f;

        if (remote.Button.d_up) {
            dZ = 1;
        }
        if (remote.Button.d_down) {
            dZ = -1;
        }
        if (remote.Button.d_right) {
            dX = 1;
        }
        if (remote.Button.d_left) {
            dX = -1;
        }

        rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        rb.AddForce(new Vector3(dX, dY, dZ) * force, ForceMode.Impulse);
    }
}
