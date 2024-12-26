using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// codice ispirato a: https://www.youtube.com/watch?v=vBWcb_0HF1c

public class FirstPersonController : MonoBehaviour
{
    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed = 5.0f;
    [SerializeField] private float sprintMultiplier = 2.0f;

    // [Header("Jump Parameters")]
    // [SerializeField] private float jumpForce = 5.0f;
    // [SerializeField] private float gravityMultiplier = 1.0f;

    [Header("Look Parameters")]
    [SerializeField] private float mouseSensivity = 0.4f;
    [SerializeField] private float upDownLookRange = 60f;

    [Header("References")]
    [SerializeField] private CharacterController characterController; // inserire il modulo Character Controller
    [SerializeField] private Camera playerCamera; // inserire il nodo PlayerCamera
    [SerializeField] private playerInputHandler playerInputHandler; // inserire /Scripts/Player/PlayerInputHandler.cs

    private Vector3 currentMovement;
    private float verticalView;
    private float CurrentSpeed => walkSpeed * (playerInputHandler.SprintTriggered ? sprintMultiplier : 1);

    void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            #if UNITY_STANDALONE
                SceneManager.LoadSceneAsync("PauseMenu");

            #endif
            #if UNITY_EDITOR
                SceneManager.LoadSceneAsync("PauseMenu");
            #endif
        }
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleView();
        Pause();
    }

    private Vector3 CalculateWorldDirection()
    {
        Vector3 inputDirection = new Vector3(playerInputHandler.MovementInput.x, 0f, playerInputHandler.MovementInput.y);
        Vector3 worldDirection = transform.TransformDirection(inputDirection);
        return worldDirection.normalized;
    }

    /* 
    // VEDERE AnimationStateController.cs:
    // SALTO FISICO SOSTITUITO CON ANIMAZIONE DEL SALTO!
    private void HandleJumping()
    {
        if (characterController.isGrounded)
        {
            currentMovement.y = -0.5f;

            if (playerInputHandler.JumpTriggered)
            {
                currentMovement.y = jumpForce;
            }
        } else {
            currentMovement.y += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
        }
    } */

    private void HandleMovement()
    {
        Vector3 worldDirection = CalculateWorldDirection();
        currentMovement.x = worldDirection.x * CurrentSpeed;
        currentMovement.z = worldDirection.z * CurrentSpeed;

        // HandleJumping();
        characterController.Move(currentMovement * Time.deltaTime);
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
