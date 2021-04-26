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
    bool patrolling, pursuing, doorNext;
    DoorScript door;

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
        if (patrolling)
        {
            if ((transform.position - nextDest).magnitude < .25)
            {
                SetNextPatrolDest();
            }

            if (doorNext)
            {
                if ((transform.position - nextDest).magnitude < 1)
                {
                    agent.ResetPath();
                    patrolling = false;
                    doorNext = false;
                    door.isOpen = true;
                    StartCoroutine(GoThroughDoor());
                }
            }
        }

        RaycastHit hit;
        if (Physics.SphereCast(head.transform.position, 1, head.transform.forward, out hit, Mathf.Infinity, guardLayer))
        {
            //print("hit");
            //   if (hit.collider.gameObject.layer.Equals("PlayerM"))
            {

                pursuing = true;
                patrolling = false;
                doorNext = false;
            }
        }

        if (pursuing)
        {
            agent.SetDestination(player.transform.position);
        }

        animator.SetBool("Patrolling", patrolling);
        animator.SetBool("Pursuing", pursuing);
    }

    IEnumerator GoThroughDoor()
    {
        yield return new WaitForSeconds(.2f);
        
        patrolling = true;
        SetNextPatrolDest();

        yield return new WaitForSeconds(2f);
        door.isOpen = false;
        print("Close");
    }

    private void SetNextPatrolDest()
    {
        nextDestIndex = (nextDestIndex + 1) % patrolCheckpoints.Length;
        nextDest = patrolCheckpoints[nextDestIndex].transform.position;
        agent.SetDestination(nextDest);
        door = DoorManagerScript.Instance.ClosestDoorToPoint(nextDest);
        if (door.transform.position == nextDest)
        {
            doorNext = true;
        }
    }
}
