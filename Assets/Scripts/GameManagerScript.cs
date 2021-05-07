using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.THIEF_CAUGHT, LoseGame);
        Messenger.AddListener(GameEvent.WIN_GAME, WinGame);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.THIEF_CAUGHT, LoseGame);
        Messenger.RemoveListener(GameEvent.WIN_GAME, WinGame);
    }

    private void LoseGame()
    {
        SceneManager.LoadScene(ApplicationManagerScript.SCENE_LOSE);
    }

    private void WinGame()
    {
        SceneManager.LoadScene(ApplicationManagerScript.SCENE_WIN);
    }
}
