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
        // TODO: Play grab sound (don't play it from an audio source on this object otherwise the sound will stop when the card is disabled)
        gameObject.SetActive(false);
    }
}
