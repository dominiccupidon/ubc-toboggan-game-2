using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Needed for SceneManager

public class SceneChanger : MonoBehaviour
{
    // Method for Main Menu
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");  // Replace with your actual scene name
    }

    // Method for Level Select Screen
    public void LevelSelectScreen()
    {
        SceneManager.LoadScene("Level Select Screen");  // Replace with your actual scene name
    }

    public void Controls()
    {
        SceneManager.LoadScene("Controls");  // Replace with your actual scene name
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");  // Replace with your actual scene name
    }
    public void Level1()
    {
        SceneManager.LoadScene("Level 1");  // Replace with your actual scene name
    }
    public void Level2()
    {
        SceneManager.LoadScene("Level 2");  // Replace with your actual scene name
    }
    public void Level3()
    {
        SceneManager.LoadScene("Level 3");  // Replace with your actual scene name
    }
    public void Level4()
    {
        SceneManager.LoadScene("Level 4");  // Replace with your actual scene name
    }
    public void Level5()
    {
        SceneManager.LoadScene("Level 5");  // Replace with your actual scene name
    }
    public void Return()
    {
        SceneManager.LoadScene("Main Menu");
    }
    
    public void Playground()
    {
        SceneManager.LoadScene("Playground");
    }
}