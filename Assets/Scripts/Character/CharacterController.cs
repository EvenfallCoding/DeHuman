using Unity.VisualScripting;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private GameInput gameInput; // riferimento a /Inputs/GameInput.cs
    private Vector2 inputMovement;
    private Vector2 inputView;

    [Header("References")]
    public Transform cameraHolder; // nodo figlio di Player, contiene il nodo Camera

    [Header("Settings")]
    public float movementSpeed = 5f; // velocita giocatore
    public float mouseSensitivity = 2f; // sensibilita visuale
    public float viewClampYMin = -70f; // clamp minimo visuale verticale
    public float viewClampYMax = 80f; // clamp massimo visuale verticale

    private float xRotation; // rotazione verticale della telecamera 
    

    private void Awake() {
        gameInput = new GameInput();
        /*  Le 4 direzioni di movimento (avanti-dietro-destra-sinistra), 
            Sono rappresentate da un vettore 2D:
                AVANTI --> X = 0, Y = 1
                DIETRO --> X = 0, Y = -1
                DESTRA --> X = 1, Y = 0
                SINISTRA --> X = -1, Y = 0
        */
        // input movimento (tasti WASD o stick sinistro)
        gameInput.Character.Movement.performed += ctx => inputMovement = ctx.ReadValue<Vector2>(); // quando si muove
        gameInput.Character.Movement.canceled += ctx => inputMovement = Vector2.zero; // quando smette di muoversi

        // input visuale (mouse o stick destro)
        gameInput.Character.View.performed += ctx => inputView = ctx.ReadValue<Vector2>();
        gameInput.Character.View.canceled += ctx => inputView = Vector2.zero;

        gameInput.Enable(); // abilita input actions
    }

    private void Update() {
        HandleView();
        HandleMovement();
    }

    private void HandleView() {
        xRotation -= inputView.y * mouseSensitivity; 
        xRotation = Mathf.Clamp(xRotation, viewClampYMin, viewClampYMax); // per evitare rotazioni di 360 gradi guardando verso alto/basso

        cameraHolder.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // ruota CameraHolder solo sull'asse X

        transform.Rotate(mouseSensitivity * inputView.x * Vector3.up);
    }

    private void HandleMovement() {
        Vector3 forward = transform.forward; // asse Z, direzione visione verso alto/basso
        Vector3 right = transform.right; // asse X, direzione visione verso destra/sinistra

        Vector3 moveDirection = (forward * inputMovement.y + right * inputMovement.x).normalized;

        transform.position += movementSpeed * Time.deltaTime * moveDirection; // muove personaggio nella direzione in cui guarda
    }
}
