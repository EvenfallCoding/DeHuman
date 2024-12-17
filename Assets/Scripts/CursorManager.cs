using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private void Start()
    {
        // blocca cursore al centro dello schermo e lo nasconde
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            // ripristina la visibilità del cursore quando applicazione perde il focus (premere ESC)
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
