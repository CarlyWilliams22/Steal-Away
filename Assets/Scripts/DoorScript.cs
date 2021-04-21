using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    Animator animator;

    private const string ANIMATOR_PROPERTY_IS_OPEN = "isOpen";

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // TODO REMOVE
        if (Input.GetKeyDown(KeyCode.C))
        {
            isOpen = false;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            isOpen = true;
        }
    }

    public bool isOpen
    {
        get => animator.GetBool(ANIMATOR_PROPERTY_IS_OPEN);
        set => animator.SetBool(ANIMATOR_PROPERTY_IS_OPEN, value);
    }
}
