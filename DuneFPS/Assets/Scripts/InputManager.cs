using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WiimoteApi;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    public static InputManager Instance
    {
        get
        {
            if (!_instance) {
                _instance = new GameObject().AddComponent<InputManager>();
                _instance.name = "InputManager";
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    #nullable enable
    public Wiimote? remote
    {
        get { 
            if (WiimoteManager.Wiimotes.Count > 0) {
                return WiimoteManager.Wiimotes[0]; 
            } else {
                return null;
            }
        }
    }
    #nullable disable

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void GetInputMethod()
    {
        WiimoteManager.FindWiimotes();
        if (WiimoteManager.Wiimotes.Count > 0) {
            WiimoteManager.Wiimotes[0].SendDataReportMode(InputDataType.REPORT_BUTTONS);
            // WiimoteManager.Wiimotes[0].SetupIRCamera(IRDataType.BASIC);
        }
    }
}
