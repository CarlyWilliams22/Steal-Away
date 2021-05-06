using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;

public class HUDManager : MonoBehaviour
{
    float startTime, elapsedTime;
    public Text timer;
    public GameObject icons, paintingIcon, keycardIcon;
    bool showIcons;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        showIcons = true;


        //****DON'T FORGET TO REMOVE THIS****
        Prefs.SetAllToDefault();
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
        Messenger.AddListener(GameEvent.OBTAINED_KEY_CARD, obtainKeycard);
        Messenger.AddListener(GameEvent.PAINTING_STOLEN, obtainPainting);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.SWITCH_PLAYER, ToggleItemIcons);
        Messenger.RemoveListener(GameEvent.OBTAINED_KEY_CARD, obtainKeycard);
        Messenger.RemoveListener(GameEvent.PAINTING_STOLEN, obtainPainting);

        Prefs.SetFloat(Prefs.Property.LastScore, elapsedTime);
    }

    private void ToggleItemIcons()
    {
        showIcons = !showIcons;
        icons.SetActive(showIcons);
    }

    void obtainKeycard()
    {
        keycardIcon.SetActive(true);
    }

    void obtainPainting()
    {
        paintingIcon.SetActive(true);
    }
}
