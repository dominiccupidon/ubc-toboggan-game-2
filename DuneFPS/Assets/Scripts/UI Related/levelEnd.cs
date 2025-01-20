using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneShifter : MonoBehaviour
{
    // This method is called when another collider enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has entered the trigger area
        if (other.CompareTag("Player"))
        {
            // Load the level select screen
            SceneManager.LoadScene("Level Select Screen");
        }
    }
}