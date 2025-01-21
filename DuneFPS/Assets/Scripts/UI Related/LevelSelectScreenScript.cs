using UnityEngine;

public class CursorManager : MonoBehaviour
{
    void Start()
    {
        // Ensure the cursor is unlocked and visible when entering this scene
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
