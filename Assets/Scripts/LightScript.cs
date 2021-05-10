using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    Light _light;

    void Start()
    {
        _light = GetComponent<Light>();
    }

    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.ALARM_SOUNDED, SwitchColor);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.ALARM_SOUNDED, SwitchColor);
    }

    void SwitchColor()
    {
        _light.color = Color.red;
    }

}
