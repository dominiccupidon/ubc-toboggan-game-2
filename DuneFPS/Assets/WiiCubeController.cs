using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WiimoteApi;

public class WiiCubeController : MonoBehaviour
{
    public Rigidbody rb;
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
            dX += 3f;
        }
        if (remote.Button.d_down) {
            dX -= 3f;
        }
        if (remote.Button.d_right) {
            dZ += 3f;
        }
        if (remote.Button.d_left) {
            dZ -= 3f;
        }

        rb.velocity = transform.TransformDirection(new Vector3(dX, dY, dZ));
    }
}
