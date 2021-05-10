using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSMScript : MonoBehaviour
{
    public AudioClip alarm;
    AudioSource a;
    bool alarmOn = false;

    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.ALARM_SOUNDED, SwitchClip);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.ALARM_SOUNDED, SwitchClip);
    }


    void SwitchClip()
    {
        if (!alarmOn)
        {
            a.Pause();
            a.clip = alarm;
            a.Play();
            alarmOn = true;
        }
    }
}
