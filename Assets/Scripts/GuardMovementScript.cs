using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardMovementScript : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    public GameObject[] patrolCheckpoints;
    public GameObject player;
    public GameObject head;
    public LayerMask guardLayer;
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
        if ((transform.position - nextDest).magnitude < .25)
        {
            nextDestIndex = (nextDestIndex + 1) % patrolCheckpoints.Length;
            nextDest = patrolCheckpoints[nextDestIndex].transform.position;
            agent.SetDestination(nextDest);
        }

        RaycastHit hit;
        if (Physics.Raycast(head.transform.position, Vector3.forward, Mathf.Infinity, guardLayer))
        {
          //  print("hit");
         //   if (hit.collider.gameObject.layer.Equals("PlayerM"))
            {
                
                pursuing = true;
            }
        }
      
        if (pursuing)
        {
            agent.SetDestination(player.transform.position);
        }

        animator.SetBool("Patrolling", patrolling);
        animator.SetBool("Pursuing", pursuing);
    }
}
