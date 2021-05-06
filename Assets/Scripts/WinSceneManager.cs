using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;

public class WinSceneManager : MonoBehaviour
{
    public Text scoreText, highScoreText;
    float lastScore, highScore;

    // Start is called before the first frame update
    void Start()
    {

        lastScore = Prefs.GetFloat(Prefs.Property.LastScore);
        highScore = Prefs.GetFloat(Prefs.Property.Highscore);
        scoreText.text = "Your time: " + System.TimeSpan.FromSeconds(lastScore).ToString("mm':'ss"); 

        if(lastScore < highScore)
        {
            Prefs.SetFloat(Prefs.Property.Highscore, lastScore);
            highScoreText.text = "New best time: " + System.TimeSpan.FromSeconds(lastScore).ToString("mm':'ss");
        }
        else
        {
            highScoreText.text = "Best time: " + System.TimeSpan.FromSeconds(highScore).ToString("mm':'ss");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
