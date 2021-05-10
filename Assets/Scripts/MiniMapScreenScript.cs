using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MiniMapScreenScript : MonoBehaviour
{
    private ColorGrading colorGrading;
    private PostProcessVolume postProcessVolume;

    // Start is called before the first frame update
    void Start()
    {
        postProcessVolume = GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings(out colorGrading);
    }

    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.ALARM_SOUNDED, OnAlarmSounded);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.ALARM_SOUNDED, OnAlarmSounded);
    }

    private void OnAlarmSounded()
    {
        colorGrading.colorFilter.value = Color.red;
    }
}
