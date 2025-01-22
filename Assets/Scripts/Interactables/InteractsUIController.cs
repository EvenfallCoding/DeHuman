using UnityEngine;

// inserire lo script in un GameObject dedicato (da chiamare LevelControl) nella scena.

// questo codice permette di modificare la interfaccia utente, per esempio quando
// si interagisce con degli oggetti o degli npc.

public class interactsUIController : MonoBehaviour
{
    [SerializeField] private GameObject interactionBox;

    // variabili modificate da ExplorableObjects.cs e LootGiverObjects.cs
    public static bool uiActive;
    public static string commandKey;
    public static string actionText;

    public static bool interact;

    private void OpenObjectInvenctory() 
    {
        // NB: AL MOMENTO BISOGNA TENERE PREMUTO, QUANDO AVREMO IL VERO INVENTARIO FUNZIONERA AL 100%
        Debug.Log("Inventario aperto");
    }

    private void Update()
    {
        if (uiActive == true && interact == false) // se oggetto vicino, senza interazione: mostra indicazioni a schermo
        {
            interactionBox.SetActive(true);
            interactionBox.GetComponent<TMPro.TMP_Text>().text = "Press [" + commandKey + "] to " + actionText;
        } 
        else if (uiActive == false && interact == true) { // se oggetto vicino, con interazione: apre inventario oggetto
            OpenObjectInvenctory();
        }
        else // oggetto lontano: togli indicazioni
        {
            interactionBox.SetActive(false);
        }
    }
}
