using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MCollisionScript : MonoBehaviour
{
    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        print(hit.collider.gameObject.tag);
        if (hit.collider.gameObject.tag.Equals("Guard"))
        {
            Messenger.Broadcast(GameEvent.THIEF_CAUGHT);
        }
    }
}
