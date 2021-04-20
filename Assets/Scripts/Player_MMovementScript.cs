using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MMovementScript : MonoBehaviour
{
    CharacterController controller;
    public float SPEED = 3;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {


        Vector3 moveVec = transform.rotation
         * (Time.deltaTime * new Vector3(SPEED * Input.GetAxis("Horizontal"), 0,
            SPEED * Input.GetAxis("Vertical")));

        controller.Move(moveVec);

        controller.Move(moveVec);

    }
}
