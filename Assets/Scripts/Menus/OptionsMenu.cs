using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public void BackBtn()
    {
        SceneManager.LoadSceneAsync("StartMenu");
    }
}