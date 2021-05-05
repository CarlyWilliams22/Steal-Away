using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    float startTime, elapsedTime;
    public Text timer;
    bool showIcons;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime = Time.time - startTime;
        timer.text = System.TimeSpan.FromSeconds(elapsedTime).ToString("mm':'ss");
    }

    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.SWITCH_PLAYER, ToggleItemIcons);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.SWITCH_PLAYER, ToggleItemIcons);
    }

    private void ToggleItemIcons()
    {

    }

    void obtainKeycard()
    {

    }

    void obtainPainting()
    {

    }
}
