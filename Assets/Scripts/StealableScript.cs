using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealableScript : MonoBehaviour
{
    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.PAINTING_STOLEN, OnPaintingStolen);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.PAINTING_STOLEN, OnPaintingStolen);
    }

    private void OnPaintingStolen()
    {
        gameObject.SetActive(false);
        Messenger.Broadcast(GameEvent.ALARM_SOUNDED);
    }
}
