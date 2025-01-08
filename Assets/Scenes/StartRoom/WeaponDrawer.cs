using UnityEngine;

public class Drawer : MonoBehaviour
{
    void OnMouseOver()
    {
        UIController.actionText = "open drawer";
        UIController.commandKey = "E";
        UIController.uiActive = true;
    }
    void OnMouseExit()
    {
        UIController.actionText = " ";
        UIController.commandKey = " ";
        UIController.uiActive = false;
    }
}
