using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void PlayBtn(){
        SceneManager.LoadSceneAsync("StartRoom");
    }
    public void OptionsBtn(){
        SceneManager.LoadSceneAsync("OptionsMenu");
    }
    public void QuitBtn(){
        #if UNITY_STANDALONE
            Application.Quit();
        #endif
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
