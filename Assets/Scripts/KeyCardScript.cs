using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCardScript : MonoBehaviour
{
    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.OBTAINED_KEY_CARD, OnObtainedKeyCard);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.OBTAINED_KEY_CARD, OnObtainedKeyCard);
    }

    private void OnObtainedKeyCard()
    {
        gameObject.SetActive(false);
    }
}
