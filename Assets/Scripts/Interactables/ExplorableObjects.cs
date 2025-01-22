 using UnityEngine;

// inserire lo script nel GameObject di un oggetto apribile/esplorabile,
// esplorabile significa che apre un inventario navigabile.

// questo codice comunica a InteractsUIController.cs quale indicazione scrivere
// a schermo (esempio: "premi E per interagire") e apre inventario in caso di interazione.

public class ExplorableObjects : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private interactsUIController interactsUIController; // inserire GameObject con /Scripts/Interactables/InteractsUIController.cs
    [SerializeField] private playerInputHandler playerInputHandler; // inserire GameObject con /Scripts/Player/PlayerInputHandler.cs

    private void OnMouseOver()
    {
        if (PlayerRaycast.toTarget < 2f) // se oggetto vicino
        {
            interactsUIController.uiActive = true; // mostra indicazioni
            interactsUIController.commandKey = "E";
            interactsUIController.actionText = "open "; // + gameObject.name se si vuole leggere nome oggetto interagibile

            if (playerInputHandler.InteractTriggered) // se interazione
            {
                interactsUIController.uiActive = false; // nasconde indicazioni
                interactsUIController.interact = true; // apre inventario
            }
            else // nessuna interazine
            {
                interactsUIController.interact = false; // tiene inventario chiuso
            }
        }
    }

    private void OnMouseExit()
    {
        interactsUIController.uiActive = false;
        interactsUIController.interact = false;
        interactsUIController.commandKey = " ";
        interactsUIController.actionText = " ";
    }
}
