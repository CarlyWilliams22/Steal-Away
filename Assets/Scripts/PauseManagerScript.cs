using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;

public class PauseManagerScript : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject inGameCursor;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Messenger.Broadcast(GameEvent.PAUSE, !pauseMenu.activeSelf);
        }
    }

    private void OnEnable()
    {
        Messenger.AddListener<bool>(GameEvent.PAUSE, OnPause);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable()
    {
        Messenger.RemoveListener<bool>(GameEvent.PAUSE, OnPause);
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnPause(bool pause)
    {
        // pause/unpause game simulation time
        Time.timeScale = pause ? 0 : 1;
        Time.fixedDeltaTime = 0.2f * Time.timeScale;

        pauseMenu.SetActive(pause);
        inGameCursor.SetActive(!pause);
        Cursor.lockState = pause ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void OnClickResume()
    {
        Messenger.Broadcast(GameEvent.PAUSE, false);
    }

    public void OnClickMainMenu()
    {
        OnPause(false);
        FindObjectOfType<ApplicationManagerScript>().OnClickMainMenu();
    }
}
