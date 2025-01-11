using UnityEngine;

// inserire lo script nel GameObject Camera del personaggio

public class PlayerRaycast : MonoBehaviour
{
    [SerializeField] private float distanceFromTarget; // [SerializeField] per poter leggere il valore e fare debugging
    public static float toTarget; // resa public per essere letta da scripts degli oggetti interagibili
    
    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit Hit))
        {
            distanceFromTarget = Hit.distance; // ottiene la distanza da quello a cui il personaggio sta puntando
            toTarget = distanceFromTarget;
        }
    }
}
