using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MMovementScript : MonoBehaviour
{
    CharacterController controller;
    public float SPEED = 3f;
    bool sneak = false;
    Vector3 normal;
    Vector3 shrink;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        normal = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        shrink = new Vector3(transform.localScale.x, transform.localScale.y*.5f, transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {


        Vector3 moveVec = transform.rotation
         * (Time.deltaTime * new Vector3(SPEED * Input.GetAxis("Horizontal"), 0,
            SPEED * Input.GetAxis("Vertical")));

        controller.Move(moveVec);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            sneak = !sneak;
            if (sneak)
            {
                transform.localScale = shrink;
                SPEED = .5f;
                controller.center = new Vector3(0, 1.2f, 0);
                animator.SetBool("Crouching", true);
            }
            else
            {
                transform.localScale = normal;
                SPEED = 3;
                controller.center = new Vector3(0, .95f, 0);
                animator.SetBool("Crouching", false);
            }
        }
        
        if(controller.velocity == Vector3.zero)
        {
            animator.SetBool("Moving", false);
        }
        else
        {
            animator.SetBool("Moving", true);
        }

    }
}
