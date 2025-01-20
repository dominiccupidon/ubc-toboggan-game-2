using UnityEngine;

public class GameOverCursorController : MonoBehaviour
{
    void Start()
    {
        // Ensure the cursor is visible and not locked when the game over scene is loaded
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
