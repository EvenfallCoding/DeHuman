using UnityEngine;

public class Drawer : MonoBehaviour
{
    void OnMouseOver()
    {
        UIController.actionText = "Open drawer";
        UIController.commandText = "Open";
        UIController.uiActive = true;
    }
    void OnMouseExit()
    {
        UIController.actionText = " ";
        UIController.commandText = " ";
        UIController.uiActive = false;
    }
}
