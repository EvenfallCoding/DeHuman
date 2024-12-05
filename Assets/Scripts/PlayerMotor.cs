using UnityEngine;

/*
QUESTA CLASSE HA LO SCOPO DI RICEVERE DA InputManager.cs GLI INPUT 
DEL GIOCATORE PER METTERE IN PRATICA DETERMINATE AZIONI
*/

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity; // vettore movimento del giocatore
    private bool isGrounded; // per verificare se sta toccando terra

    public float speed = 5f;
    public float gravity = -9.81f;
    public float jump = 2f;

    private void Start() {
        controller = GetComponent<CharacterController>();
    }

    private void Update() {
        isGrounded = controller.isGrounded;
    }

    public void Movements(Vector2 input) { // riceve input da InputManager.cs e applica le azioni al character controller
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(speed * Time.deltaTime * transform.TransformDirection(moveDirection)); // applica movimento WASD

        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0) {
            playerVelocity.y = -2f;
        }
        controller.Move(playerVelocity * Time.deltaTime); // applica gravità
    }

    public void Jump() {
        if (isGrounded) {
            playerVelocity.y = Mathf.Sqrt(jump * -jump * gravity);
        }
    }
}
