using UnityEngine;
using TMPro; // For TextMeshPro
using UnityEngine.SceneManagement; // For scene management

[RequireComponent(typeof(BoxCollider))]
public class VisibleCollider : MonoBehaviour
{
    public TMP_Text warningText; // Reference to a TextMeshPro UI element
    private int collisionCount = 0; // Tracks player collisions

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Check if the collider belongs to the player
        {
            collisionCount++;

            if (collisionCount < 4)
            {
                DisplayMessage("Walking off the map is highly discouraged!", 2.5f);
            }
            else if (collisionCount >= 4)
            {
                DisplayMessage("You Goofy Goober", 2.5f);
                Invoke(nameof(RestartLevel), 1f);
            }
        }
    }

    private void DisplayMessage(string message, float duration)
    {
        if (warningText != null)
        {
            warningText.text = message;
            warningText.enabled = true;

            // Hide the message after the specified duration
            Invoke(nameof(HideWarning), duration);
        }
    }

    private void HideWarning()
    {
        if (warningText != null)
        {
            warningText.enabled = false;
        }
    }

    private void RestartLevel()
    {
        // Restart the level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
