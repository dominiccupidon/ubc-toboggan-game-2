using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using WiimoteApi;

public class PauseManager : MonoBehaviour
{
    bool isPaused = false;
    bool isWiimoteConnected;
    Color notSelected;
    Color selected;
    Wiimote remote;
    [SerializeField ]GameObject pausePanel;
    [SerializeField] GameObject exitPanel;
    [SerializeField] GameObject controlsPanel;
    [SerializeField] GameObject gameControlButtons;
    [SerializeField] GameObject UIControlButtons;
    [SerializeField] Image selectUI;
    [SerializeField] Image selectGame;

    [SerializeField] GameObject wiimote3D;
    [SerializeField] Material BaseWiimote;
    [SerializeField] Material WiiA;
    [SerializeField] Material WiiB;
    [SerializeField] Material WiiDpad;
    [SerializeField] Material WiiHome;

    [SerializeField] GameObject gunModel;


    Material[] BaseWiimote2 = new Material[7];
    Material[] WiiA2 = new Material[7];
    Material[] WiiB2 = new Material[7];
    Material[] WiiDpad2 = new Material[7];
    Material[] WiiHome2 = new Material[7];

    // Start is called before the first frame update
    void Start()
    {
        notSelected = new Color(255f/255, 187f/255, 109f/255);
        selected = new Color(255f/255, 167f/255, 66f/255);
        pausePanel.SetActive(false);
        if (InputManager.Instance.remote != null) {
            remote = InputManager.Instance.remote;
            isWiimoteConnected = true;
        }

        for (int i = 0; i < 7; i++)
        {
            BaseWiimote2[i] = BaseWiimote;
            WiiA2[i] = WiiA;
            WiiB2[i] = WiiB;
            WiiDpad2[i] = WiiDpad;
            WiiHome2[i] = WiiHome;
        }
    }
    void Update()
    {
        bool isHomeButtonPressed = false;
        if (isWiimoteConnected) {
            int ret;
            do {
                ret = remote.ReadWiimoteData();
            } while (ret > 0);
            isHomeButtonPressed = remote.Button.home;
        }

        if (Input.GetKeyDown(KeyCode.Space) || isHomeButtonPressed){
            PauseFunction();
        }
    }

    public void PauseFunction() 
    {
        if (isPaused)
        {
            pausePanel.SetActive(false);
            controlsPanel.SetActive(false);
            Time.timeScale = 1;
            Cursor.visible = false;
        }
        else {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
            Cursor.visible = true;
        }
        isPaused = !isPaused;
    }

    public bool IsPaused() 
    {
        return isPaused;
    }

    public void GotoControls() 
    {
        controlsPanel.SetActive(true);
        pausePanel.SetActive(false);
        wiimote3D.SetActive(true);
        gunModel.SetActive(false);
        wiimote3D.transform.localRotation = Quaternion.Euler(-90, 180, 0);
        wiimote3D.GetComponent<MeshRenderer>().materials = BaseWiimote2;
    }
    public void MainPause() 
    {
        pausePanel.SetActive(true);
        controlsPanel.SetActive(false);
    }
    public void MainMenu()
    {
        exitPanel.SetActive(true);
    }

    public void GotoMenu()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SceneChanger>().MainMenu();
    }

    public void StayInLevel() 
    {
        exitPanel.SetActive(false);
    }

    //Controls Functions
    public void GameControls() 
    {
        gameControlButtons.SetActive(true);
        UIControlButtons.SetActive(false);
        wiimote3D.SetActive(true);
        gunModel.SetActive(false);
        selectGame.color = selected;
        selectUI.color = notSelected;
        wiimote3D.GetComponent<MeshRenderer>().materials = BaseWiimote2;
    }
    public void UIControls() 
    {
        gameControlButtons.SetActive(false);
        UIControlButtons.SetActive(true);
        wiimote3D.SetActive(true);
        gunModel.SetActive(false);
        selectGame.color = notSelected;
        selectUI.color = selected;
        wiimote3D.GetComponent<MeshRenderer>().materials = BaseWiimote2;
    }

    public void HighlightA() 
    {
        wiimote3D.SetActive(true);
        gunModel.SetActive(false);
        wiimote3D.transform.localRotation = Quaternion.Euler(-90, 180, 0);
        wiimote3D.GetComponent<MeshRenderer>().materials = WiiA2;
    }
    public void HighlightB()
    {
        wiimote3D.SetActive(true);
        gunModel.SetActive(false);
        wiimote3D.transform.localRotation = Quaternion.Euler(-90, 180, -90);
        wiimote3D.GetComponent<MeshRenderer>().materials = WiiB2;
    }
    public void HighlightDPad()
    {
        wiimote3D.SetActive(true);
        gunModel.SetActive(false);
        wiimote3D.transform.localRotation = Quaternion.Euler(-90, 180, 0);
        wiimote3D.GetComponent<MeshRenderer>().materials = WiiDpad2;
    }
    public void HighlightHome()
    {
        wiimote3D.SetActive(true);
        gunModel.SetActive(false);
        wiimote3D.transform.localRotation = Quaternion.Euler(-90, 180, 0);
        wiimote3D.GetComponent<MeshRenderer>().materials = WiiHome2;
    }
    public void HighlightTrigger() 
    {
        wiimote3D.SetActive(false);
        gunModel.SetActive(true);
    }
}
