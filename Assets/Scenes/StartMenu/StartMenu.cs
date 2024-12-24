using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void PlayBtn(){
        SceneManager.LoadSceneAsync("Playground");
    }
}
