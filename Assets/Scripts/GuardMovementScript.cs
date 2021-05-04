﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardMovementScript : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    public GameObject[] patrolCheckpoints, patrolCheckpoints2;
    GameObject[] currentPatrol;
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
        SetNextPatrolDest();
    }

    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.ALARM_SOUNDED,Pursue);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.ALARM_SOUNDED, Pursue);
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
                    if (door.isOpen)
                    {
                        Messenger.Broadcast(GameEvent.ALARM_SOUNDED);
                    }
                    agent.ResetPath();
                    patrolling = false;
                    doorNext = false;
                    door.isOpen = true;
                    StartCoroutine(GoThroughDoor(door));
                }
            }
        }

        if (pursuing)
        {
            agent.SetDestination(player.transform.position);
        }

        animator.SetBool("Patrolling", patrolling);
        animator.SetBool("Pursuing", pursuing);
    }

    IEnumerator GoThroughDoor(DoorScript door)
    {
        yield return new WaitForSeconds(.2f);
        
        patrolling = true;
        SetNextPatrolDest();

        yield return new WaitForSeconds(2f);
        door.isOpen = false;
        //print("Close");
    }

    private void SetNextPatrolDest()
    {
        nextDestIndex++;
        if(currentPatrol == null || nextDestIndex >= currentPatrol.Length)
        {
            if(Random.Range(0,2) == 0)
            {
                currentPatrol = patrolCheckpoints;
            }
            else
            {
                currentPatrol = patrolCheckpoints2;
            }
            nextDestIndex = 0;
        }
        nextDest = currentPatrol[nextDestIndex].transform.position;
        agent.SetDestination(nextDest);
        door = DoorManagerScript.Instance.ClosestDoorToPoint(nextDest);
        if (door.transform.position == nextDest)
        {
            doorNext = true;
        }
    }

    private void Pursue()
    {
        pursuing = true;
        patrolling = false;
        doorNext = false;
    }

    
}
