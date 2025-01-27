using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void SaveAndQuitBtn()
    {
        SceneManager.LoadSceneAsync("StartMenu");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void BackToGameBtn()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        this.gameObject.SetActive(false);
    }

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
