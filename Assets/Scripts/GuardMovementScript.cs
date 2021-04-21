using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardMovementScript : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    public GameObject[] patrolCheckpoints;
    int nextDestIndex;
    Vector3 nextDest;
    bool patrolling, pursuing;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        patrolling = true;
        nextDestIndex = 0;
        nextDest = patrolCheckpoints[nextDestIndex].transform.position;
        agent.SetDestination(nextDest);
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position - nextDest).magnitude < .5)
        {
            nextDestIndex = (nextDestIndex + 1) % patrolCheckpoints.Length;
            nextDest = patrolCheckpoints[nextDestIndex].transform.position;
            agent.SetDestination(nextDest);
        }


        animator.SetBool("Patrolling", patrolling);
        animator.SetBool("Pursuing", pursuing);
    }
}
