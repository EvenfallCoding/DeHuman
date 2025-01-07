using TMPro;
using UnityEditor;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    public static float distanceFromTarget;
    [SerializeField] float toTarget;
    private void Update()
    {
        RaycastHit Hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Hit))
        {
            toTarget = Hit.distance;
            distanceFromTarget = toTarget;
        }
    }
}
