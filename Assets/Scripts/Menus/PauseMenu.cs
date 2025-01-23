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
        this.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
