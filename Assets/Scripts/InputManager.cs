using UnityEngine;

/*
QUESTA CLASSE HA LO SCOPO DI RICEVERE IN INPUT I COMANDI DEL GIOCATORE
E MANDARLI A PlayerMotor.cs PER METTERE IN PRATICA DETERMINATE AZIONI
*/

public class InputManager : MonoBehaviour
{
    private GameInput gameInput; // oggetto definito dalla classe in /Assets/Input/GameInput.cs
	private GameInput.PlayerActions player; // riferito alla action map chiamata Player, contenuto in /Assets/Input/GameInput 

	private PlayerMotor motor; // riferito a PlayerMotor.cs

	private PlayerLook look; // riferito a PlayerMotor.cs
	
	private void Awake() {
		gameInput = new GameInput(); // creo l'oggetto dalla variabile preparata (vedi sopra)
		player = gameInput.Player; // ricavo dalla action map Player le azioni e le collego al oggetto player (vedi sopra)

		look = GetComponent<PlayerLook>();

		motor = GetComponent<PlayerMotor>();
		// ogni volta che salta utilizza una callback context (ctx) e può essere di 3 tipi: performed, started, canceled.
		// in questo caos performed perchè avviene solo quando salta. Rende il gioco più fluido
		player.Jump.performed += ctx => motor.Jump();
	}

	void FixedUpdate() {
		motor.Movements(player.Move.ReadValue<Vector2>()); // Movements() contenuta in PlayerMotor.cs
	}

	void LateUpdate() {
		look.Look(player.Look.ReadValue<Vector2>()); // Look() contenuta in PlayeLook.cs
	}

	private void OnEnable() {
		player.Enable();
	}

	private void OnDisable() {
		player.Disable();
	}
}
