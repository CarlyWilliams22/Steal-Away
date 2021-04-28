using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorScreenScript : MonoBehaviour
{
    public GameObject _camera = null;

    private void OnBecameVisible()
    {
        Messenger.Broadcast(GameEvent.MONITOR_VISIBLE, _camera, true);
    }

    private void OnBecameInvisible()
    {
        Messenger.Broadcast(GameEvent.MONITOR_VISIBLE, _camera, false);
    }
}
