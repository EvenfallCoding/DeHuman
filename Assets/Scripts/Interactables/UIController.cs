using UnityEngine;

public class UIController : MonoBehaviour
{
    public static string actionText;
    public static string commandKey;
    public static bool uiActive;
    [SerializeField] GameObject interactionBox;

    void Update()
    {
        if (uiActive == true)
        {
            interactionBox.SetActive(true);
            interactionBox.GetComponent<TMPro.TMP_Text>().text = "Press [" + commandKey + "] to " + actionText;
        }
        else
        {
            interactionBox.SetActive(false);
        }
    }
}
