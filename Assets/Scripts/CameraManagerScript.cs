using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManagerScript : MonoBehaviour
{
    public GameObject[] vanCameras;
    private bool _enabled = false;

    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.SWITCH_PLAYER, Switch);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.SWITCH_PLAYER, Switch);
    }

    private void Switch()
    {
        _enabled = !_enabled;
        foreach (GameObject cam in vanCameras)
        {
            cam.SetActive(_enabled);
        }
    }
}
