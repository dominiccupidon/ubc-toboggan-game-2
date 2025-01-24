using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management
using UnityEngine.UI; // Required for UI elements

public class PlayerLives : MonoBehaviour
{
    public int lives = 3; // Total lives
    public string gameOverSceneName = "GameOver"; // Name of the Game Over scene
    public float lifeRegainTime = 5f; // Time before the player gets a life back
    private float lastHitTime; // Time of the last hit

    public Image[] heartImages; // Array of heart image UI elements
    public Sprite fullHeartSprite; // Full heart sprite
    public Sprite emptyHeartSprite; // Empty heart sprite

    private void Start()
    {
        lastHitTime = Time.time; // Initialize the time when the game starts
        UpdateHeartUI(); // Initialize heart UI
    }

    private void Update()
    {
        // Check if the player has not been hit for 'lifeRegainTime' seconds
        if (Time.time - lastHitTime >= lifeRegainTime && lives < 3)
        {
            lives++;
            lastHitTime = Time.time; // Reset the timer when a life is regained
            Debug.Log("Player regained a life! Lives remaining: " + lives);
            UpdateHeartUI(); // Update the heart UI when a life is regained
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // Destroy the bullet
        if (other.gameObject.CompareTag("enemyBullet")) 
        {
            Destroy(other.gameObject);
            // Reduce player's lives
            lives--;

            // Update the last hit time
            lastHitTime = Time.time;

            // Check if player has no more lives
            if (lives <= 0)
            {
                // Player dies; transition to Game Over scene
                SceneManager.LoadScene(gameOverSceneName);
            }
            else
            {
                Debug.Log("Player hit! Lives remaining: " + lives);
            }

            UpdateHeartUI(); // Update the heart UI when a life is lost
        }
        



    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with an enemy bullet
        if (collision.gameObject.CompareTag("enemyBullet"))
        {
            // Destroy the bullet
            Destroy(collision.gameObject);

            // Reduce player's lives
            lives--;

            // Update the last hit time
            lastHitTime = Time.time;

            // Check if player has no more lives
            if (lives <= 0)
            {
                // Player dies; transition to Game Over scene
                SceneManager.LoadScene(gameOverSceneName);
            }
            else
            {
                Debug.Log("Player hit! Lives remaining: " + lives);
            }

            UpdateHeartUI(); // Update the heart UI when a life is lost
        }
    }
    */

    // Updates the heart UI based on the player's current lives
    private void UpdateHeartUI()
    {
        // Loop through the heart image array and update each heart based on the player's lives
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < lives)
            {
                heartImages[i].sprite = fullHeartSprite; // Show full heart
            }
            else
            {
                heartImages[i].sprite = emptyHeartSprite; // Show empty heart
            }
        }
    }
}
