using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void SaveAndQuitBtn()
    {
        SceneManager.LoadSceneAsync("StartMenu");
    }
    public void BackToGameBtn()
    {
        SceneManager.LoadSceneAsync("Playground");
    }

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
