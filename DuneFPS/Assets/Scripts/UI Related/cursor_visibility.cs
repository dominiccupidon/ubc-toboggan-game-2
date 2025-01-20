using UnityEngine;

public class ShowCursorOnLevelSelect : MonoBehaviour
{
    void Start()
    {
        // Unlock and show the cursor when the level select screen loads
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}