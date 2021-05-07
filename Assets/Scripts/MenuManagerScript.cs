using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManagerScript : MonoBehaviour
{
    public GameObject menu, instructions;


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
