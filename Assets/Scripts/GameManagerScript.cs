using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.THIEF_CAUGHT, LoseGame);
    }

    private void OnDisable()
    {
        Messenger.AddListener(GameEvent.THIEF_CAUGHT, LoseGame);
    }

    private void LoseGame()
    {
        SceneManager.LoadScene("LosingScene");
    }

    private void WinGame()
    {
        SceneManager.LoadScene("WinScene");
    }
}
