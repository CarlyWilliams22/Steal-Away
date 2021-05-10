using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MMovementScript : MonoBehaviour
{
    CharacterController controller;
    public GameObject _camera;
    public AudioClip crouchAudio;
    public float SPEED = 3f;
    bool sneak = false;
    Vector3 normal;
    Vector3 shrink;
    Animator animator;
    AudioSource a;
    bool currCharacter = true;
    bool hasStolenPainting;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        a = GetComponent<AudioSource>();
        normal = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        shrink = new Vector3(transform.localScale.x, transform.localScale.y*.5f, transform.localScale.z);
        animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
    }

    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.SWITCH_PLAYER, Switch);
        Messenger.AddListener(GameEvent.PAINTING_STOLEN, OnPaintingStolen);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.SWITCH_PLAYER, Switch);
        Messenger.RemoveListener(GameEvent.PAINTING_STOLEN, OnPaintingStolen);
    }

    // Update is called once per frame
    void Update()
    {

        if (currCharacter)
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
                    a.PlayOneShot(crouchAudio);
                }
                else
                {
                    transform.localScale = normal;
                    SPEED = 3;
                    controller.center = new Vector3(0, .95f, 0);
                    animator.SetBool("Crouching", false);
                    a.PlayOneShot(crouchAudio);
                }
            }
        }

        if (controller.velocity == Vector3.zero)
        {
            animator.SetBool("Moving", false);
        }
        else
        {
            animator.SetBool("Moving", true);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Messenger.Broadcast(GameEvent.SWITCH_PLAYER);
        }

    }

    private void Switch()
    {
        currCharacter = !currCharacter;
        _camera.SetActive(currCharacter);   
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "NearVan":
                if (hasStolenPainting)
                {
                    Messenger.Broadcast(GameEvent.WIN_GAME);
                }
                break;
        }
    }

    private void OnPaintingStolen()
    {
        hasStolenPainting = true;
    }
}
