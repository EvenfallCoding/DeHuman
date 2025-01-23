using UnityEngine;
using UnityEngine.SceneManagement;

// codice ispirato da: https://www.youtube.com/watch?v=vBWcb_0HF1c

// inserire lo script nel nodo principale del personaggio.

// questo codice permette la gestione degli input (ottenuti da PlayerInputHandler.cs)
// e le relative azioni, come interazioni e movimenti.

public class FirstPersonController : MonoBehaviour
{
    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed = 5.0f;
    [SerializeField] private float sprintMultiplier = 2.0f;

    [Header("Jump Parameters")]
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float gravityMultiplier = 1.0f;

    [Header("Look Parameters")]
    [SerializeField] private float mouseSensivity = 0.4f;
    [SerializeField] private float upDownLookRange = 60f;

    [Header("References")]
    [SerializeField] private CharacterController characterController; // inserire il modulo Character Controller
    [SerializeField] private Camera playerCamera; // inserire il nodo PlayerCamera
    [SerializeField] private playerInputHandler playerInputHandler; // inserire GameObject con /Scripts/Player/PlayerInputHandler.cs
    [SerializeField] private Animator animator; // inserire il modulo Animator
    [SerializeField] private GameObject PauseMenu;

    private Vector3 currentMovement;
    private float verticalView;
    private float CurrentSpeed => walkSpeed * (playerInputHandler.SprintTriggered ? sprintMultiplier : 1);

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Update()
    {
        HandleView();
        HandleMovement();
        HandlePause();
    }

    private void HandlePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            #if UNITY_STANDALONE
                PauseMenu.SetActive(true);
                Time.timeScale = 0f;
            #endif
            #if UNITY_EDITOR
                PauseMenu.SetActive(true);
                Time.timeScale = 0f;
            #endif
        }
    }

    private void HandleJumping()
    {
        if (characterController.isGrounded) // i salti devono partire da terra (per evitare doppi salti in aria)
        {
            if (playerInputHandler.JumpTriggered) // premuto tasto per saltare
            {
                if (playerInputHandler.MovementInput.x == 0 && playerInputHandler.MovementInput.y == 0) // da fermo
                {
                    // currentMovement.y = -0.5f;
                    animator.SetBool("isJumping", true);
                    currentMovement.y = jumpForce;
                }
                else if (playerInputHandler.MovementInput.x > 0 || playerInputHandler.MovementInput.y > 0) // in camminata
                {
                    animator.SetBool("isJumpingWalking", true);
                    currentMovement.y = jumpForce;
                }
            }
            else // tasto per saltare non premuto
            {
                animator.SetBool("isJumping", false);
                animator.SetBool("isJumpingWalking", false);
            }
        }
        else // non sta toccando terra
        {
            currentMovement.y += Physics.gravity.y * gravityMultiplier * Time.deltaTime; // applica gravita
        }
    }

    private Vector3 CalculateWorldDirection()
    {
        Vector3 inputDirection = new Vector3(playerInputHandler.MovementInput.x, 0f, playerInputHandler.MovementInput.y);
        Vector3 worldDirection = transform.TransformDirection(inputDirection);
        return worldDirection.normalized;
    }

    private void HandleMovement()
    {
        Vector3 worldDirection = CalculateWorldDirection();
        currentMovement.x = worldDirection.x * CurrentSpeed;
        currentMovement.z = worldDirection.z * CurrentSpeed;
        
        if (playerInputHandler.MovementInput.x == 0 && playerInputHandler.MovementInput.y == 0) // fermo
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isWalkingBackwards", false);
            characterController.Move(currentMovement * Time.deltaTime);
        }
        if (playerInputHandler.MovementInput.x > 0 || playerInputHandler.MovementInput.y > 0) // camminata in avanti
        {
            animator.SetBool("isWalking", true);
            characterController.Move(currentMovement * Time.deltaTime);
        }
        if (playerInputHandler.MovementInput.x < 0 || playerInputHandler.MovementInput.y < 0) // camminata indietro 
        {
            animator.SetBool("isWalkingBackwards", true);
            characterController.Move(currentMovement * Time.deltaTime);
        }

        HandleJumping();
    }

    private void ApplyHorizontalView(float rotationAmount)
    {
        transform.Rotate(0, rotationAmount, 0);
    }

    private void ApplyVerticalView(float rotationAmount)
    {
        verticalView = Mathf.Clamp(verticalView - rotationAmount, -upDownLookRange, upDownLookRange);
        playerCamera.transform.localRotation = Quaternion.Euler(verticalView, 0, 0);
    }

    private void HandleView()
    {
        float mouseXRotation = playerInputHandler.ViewInput.x * mouseSensivity;
        float mouseYRotation = playerInputHandler.ViewInput.y * mouseSensivity;

        ApplyHorizontalView(mouseXRotation);
        ApplyVerticalView(mouseYRotation);
    }
}
