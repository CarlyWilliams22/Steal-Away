using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    Animator animator;
    AudioSource a;

    private const string ANIMATOR_PROPERTY_IS_OPEN = "isOpen";

    public GameObject cardRequiredText1;
    public GameObject cardRequiredText2;
    public float messageTimeout;
    public float doorCloseTimeout;

    private IEnumerator hideMessageCoroutine = null;
    private IEnumerator doorCloseCoroutine = null;

    void Start()
    {
        animator = GetComponent<Animator>();
        a = GetComponent<AudioSource>();
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

    public void OnPlayerClickKeypad()
    {
        if (!DoorManagerScript.Instance.playerHasKeyCard)
        {
            ShowCardRequiredMessage(true);
            if (hideMessageCoroutine != null)
            {
                StopCoroutine(hideMessageCoroutine);
            }
            hideMessageCoroutine = HideMessageIn(messageTimeout);
            StartCoroutine(hideMessageCoroutine);
        } else
        {
            PlaySound();
            isOpen = !isOpen;
            if (doorCloseCoroutine != null)
            {
                StopCoroutine(doorCloseCoroutine);
            }
            doorCloseCoroutine = CloseDoorIn(doorCloseTimeout);
            StartCoroutine(doorCloseCoroutine);
        }
    }

    private void ShowCardRequiredMessage(bool show)
    {
        cardRequiredText1.SetActive(show);
        cardRequiredText2.SetActive(show);
    }

    private IEnumerator HideMessageIn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ShowCardRequiredMessage(false);
    }

    private IEnumerator CloseDoorIn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        isOpen = false;
    }

    public void PlaySound()
    {
        a.Play();
    }
}
