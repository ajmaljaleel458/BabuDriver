using UnityEngine;

public class CursorLockManager : MonoBehaviour
{
    // Set to true if you want the cursor to be locked and hidden, false otherwise
    public bool lockCursorOnStart = true;

    void Start()
    {
        if (lockCursorOnStart)
        {
            LockAndHideCursor();
        }
    }

    void Update()
    {
        // Optional: Unlock the cursor when the player presses the 'Escape' key or any other input
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnlockAndShowCursor();
        }
    }

    // Lock and hide the cursor
    public void LockAndHideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;  // Locks the cursor to the center of the screen
        Cursor.visible = false;                    // Hides the cursor
    }

    // Unlock and show the cursor
    public void UnlockAndShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;    // Unlocks the cursor
        Cursor.visible = true;                     // Shows the cursor
    }

    // Optional: Detect when the application gains or loses focus
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            // Lock and hide the cursor when the game is focused
            LockAndHideCursor();
        }
        else
        {
            // Unlock and show the cursor when the game loses focus
            UnlockAndShowCursor();
        }
    }
}
