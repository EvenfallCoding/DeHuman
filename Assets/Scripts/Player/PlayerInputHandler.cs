using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// codice ispirato a: https://www.youtube.com/watch?v=vBWcb_0HF1c

public class playerInputHandler : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls; // inserire /Inputs/PlayerInput.inputactions

    [Header("Action Map Name Reference")] // NOMI DELLE ACTION MAP CONTENUTE IN /Inputs/PlayerInput.inputactions
    [SerializeField] private string characterActionMapName = "Character";
    
    [Header("Action Name References")] // NOMI DELLE ACTIONS CONTENUTE NELLE ACTION MAP
    // ACTION MAP: Character
    [SerializeField] private string movement = "Movement";
    [SerializeField] private string view = "View";
    [SerializeField] private string jump = "Jump";
    [SerializeField] private string sprint = "Sprint";
    [SerializeField] private string crouch = "Crouch";
    [SerializeField] private string interact = "Interact";
    [SerializeField] private string attack = "Attack";

    private InputAction movementAction;
    private InputAction viewAction;
    private InputAction jumpAction;
    private InputAction sprintAction;
    private InputAction crouchAction;
    private InputAction interactAction;
    private InputAction attackAction;

    public Vector2 MovementInput {  get; private set; }
    public Vector2 ViewInput { get; private set; }
    public static bool JumpTriggered { get; private set; }
    public bool SprintTriggered { get; private set; }
    public bool CrouchTriggered { get; private set; }
    public bool InteractTriggered { get; private set; }
    public bool AttackTriggered { get; private set; }

    private void Awake()
    {
        InputActionMap characterMapReference = playerControls.FindActionMap(characterActionMapName);

        movementAction = characterMapReference.FindAction(movement);
        viewAction = characterMapReference.FindAction(view);
        jumpAction = characterMapReference.FindAction(jump);
        sprintAction = characterMapReference.FindAction(sprint);
        crouchAction = characterMapReference.FindAction(crouch);
        interactAction = characterMapReference.FindAction(interact);
        attackAction = characterMapReference.FindAction(attack);

        SubscribeActionValuesToInputEvents();
    }

    private void SubscribeActionValuesToInputEvents()
    {
        movementAction.performed += inputInfo => MovementInput = inputInfo.ReadValue<Vector2>();
        movementAction.canceled += inputInfo => MovementInput = Vector2.zero;

        viewAction.performed += inputInfo => ViewInput = inputInfo.ReadValue<Vector2>();
        viewAction.canceled += inputInfo => ViewInput = Vector2.zero;

        jumpAction.performed += inputInfo => JumpTriggered = true;
        jumpAction.canceled += inputInfo => JumpTriggered = false;

        sprintAction.performed += inputInfo => SprintTriggered = true;
        sprintAction.canceled += inputInfo => SprintTriggered = false;

        crouchAction.performed += inputInfo => CrouchTriggered = true;
        crouchAction.canceled += inputInfo => CrouchTriggered = false;

        interactAction.performed += inputInfo => InteractTriggered = true;
        interactAction.canceled += inputInfo => InteractTriggered = false;

        attackAction.performed += inputInfo => AttackTriggered = true;
        attackAction.canceled += inputInfo => AttackTriggered = false;
    }

    private void OnEnable()
    {
        playerControls.FindActionMap(characterActionMapName).Enable();
    }

    private void OnDisable()
    {
        playerControls.FindActionMap(characterActionMapName).Disable();
    }
}