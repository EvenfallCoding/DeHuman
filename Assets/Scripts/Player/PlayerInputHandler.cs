using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

// ispirato da: https://www.youtube.com/watch?v=vBWcb_0HF1c

// inserire lo script in nodo/GameObject dedicato, figlio del GameObject principale del personaggio.

// questo codice permette una lettura ottimale degli input.

public class playerInputHandler : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls; // inserire /Inputs/PlayerInput.inputactions
    
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
        InputActionMap characterActionMap = playerControls.FindActionMap("Character");

        movementAction = characterActionMap.FindAction(movement);
        viewAction = characterActionMap.FindAction(view);
        jumpAction = characterActionMap.FindAction(jump);
        sprintAction = characterActionMap.FindAction(sprint);
        crouchAction = characterActionMap.FindAction(crouch);
        interactAction = characterActionMap.FindAction(interact);
        attackAction = characterActionMap.FindAction(attack);

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
        playerControls.FindActionMap("Character").Enable();
    }

    private void OnDisable()
    {
        playerControls.FindActionMap("Character").Disable();
    }
}