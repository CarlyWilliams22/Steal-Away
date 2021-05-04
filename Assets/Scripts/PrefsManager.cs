using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class PrefsManager : MonoBehaviour
{
    private void OnEnable()
    {
        Messenger.AddListener<float>(GameEvent.MOUSE_SENSITIVITY_CHANGE, OnMouseSensitivityChange);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener<float>(GameEvent.MOUSE_SENSITIVITY_CHANGE, OnMouseSensitivityChange);
    }

    private void OnMouseSensitivityChange(float value)
    {
        Prefs.SetFloat(Prefs.Property.MouseSensitivity, value);
    }
}
