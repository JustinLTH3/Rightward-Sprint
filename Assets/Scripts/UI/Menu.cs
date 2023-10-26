using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public static void QuitGame()
    {
        Application.Quit();
    }
    public static void QuitToMainmenu()
    {
        LevelManager.LoadMainmenu();
        //make sure the timescale is 1 when quiting to Main menu.
        Time.timeScale = 1.0f;
    }
}
