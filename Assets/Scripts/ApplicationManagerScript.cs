using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationManagerScript : MonoBehaviour
{
    private static string SCENE_MAIN_MENU = "MenuScene";
    private static string SCENE_GAME = "GameScene";

    private static ApplicationManagerScript _instance = null;

    public static ApplicationManagerScript Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene(SCENE_GAME);
    }

    public void OnClickMainMenu()
    {
        SceneManager.LoadScene(SCENE_MAIN_MENU);
    }

    public void OnClickQuit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        #if UNITY_STANDALONE
            Application.Quit();
        #endif
    }
}
