using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;

public class MenuManagerScript : MonoBehaviour
{
    public GameObject menu, instructions;
    public Text highScoreText;

    private void Start()
    {
        if (Prefs.IsDefaultValue(Prefs.Property.Highscore))
        {
            highScoreText.text = "";
        }
        else
        { 
            highScoreText.text = "Best time: " + System.TimeSpan.FromSeconds(Prefs.GetFloat(Prefs.Property.Highscore)).ToString("mm':'ss");
        }

    }


    public void OnClickInstructions()
    {
        menu.SetActive(false);
        instructions.SetActive(true);
    }

    public void OnClickMenu()
    {
        menu.SetActive(true);
        instructions.SetActive(false);
    }
}
