using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    bool isPaused = false;
    Color notSelected;
    Color selected;
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
        //Remove this
        if (Input.GetKeyDown(KeyCode.Space)){
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
        }
        else {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
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
        selectGame.color = selected;
        selectUI.color = notSelected;
        wiimote3D.GetComponent<MeshRenderer>().materials = BaseWiimote2;
    }
    public void UIControls() 
    {
        gameControlButtons.SetActive(false);
        UIControlButtons.SetActive(true);
        selectGame.color = notSelected;
        selectUI.color = selected;
        wiimote3D.GetComponent<MeshRenderer>().materials = BaseWiimote2;
    }

    public void HighlightA() 
    {
        wiimote3D.transform.localRotation = Quaternion.Euler(-90, 180, 0);
        wiimote3D.GetComponent<MeshRenderer>().materials = WiiA2;
    }
    public void HighlightB()
    {
        wiimote3D.transform.localRotation = Quaternion.Euler(-90, 180, -90);
        wiimote3D.GetComponent<MeshRenderer>().materials = WiiB2;
    }
    public void HighlightDPad()
    {
        wiimote3D.transform.localRotation = Quaternion.Euler(-90, 180, 0);
        wiimote3D.GetComponent<MeshRenderer>().materials = WiiDpad2;
    }
    public void HighlightHome()
    {
        wiimote3D.transform.localRotation = Quaternion.Euler(-90, 180, 0);
        wiimote3D.GetComponent<MeshRenderer>().materials = WiiHome2;
    }
}