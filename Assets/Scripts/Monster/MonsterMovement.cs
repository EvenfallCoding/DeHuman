using UnityEngine;
using UnityEngine.AI;

public class monsterMovement : MonoBehaviour
{
    public Transform Target;
    public NavMeshAgent Monster;

    void Update()
    {
        Monster.destination = Target.position;
    }
}