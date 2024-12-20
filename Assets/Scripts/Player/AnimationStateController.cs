using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private Animator animator; // inserire il modulo Animator
    [SerializeField] private playerInputHandler playerInputHandler; // inserire /Scripts/Player/PlayerInputHandler.cs

    void FixedUpdate()
    {
        HandleJumping();
        HandleWalking();
    }

    private void HandleJumping()
    {
        if (playerInputHandler.JumpTriggered)
        {
            animator.SetBool("isJumping", true);
        } 
        else {
            animator.SetBool("isJumping", false);
        }
    }

    private void HandleWalking()
    {
        if (playerInputHandler.MovementInput.x > 0 || playerInputHandler.MovementInput.y > 0) // camminata in avanti
        {
            animator.SetBool("isWalking", true);
            if (playerInputHandler.JumpTriggered) // se durante la camminata prova a saltare
            {
                animator.SetBool("isJumpingWalking", true);
            } else
            {
                animator.SetBool("isJumpingWalking", false);
            }
        } 
        else if (playerInputHandler.MovementInput.x < 0 || playerInputHandler.MovementInput.y < 0) // camminata indietro 
        {
            animator.SetBool("isWalkingBackwards", true);
        } 
        else { // fermo
            animator.SetBool("isWalking", false);
            animator.SetBool("isWalkingBackwards", false);
        }
    }
}
