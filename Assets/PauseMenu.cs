using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public static bool paused = false;
    public GameObject pauseMenuUI;
    public GameObject pauseButton;

    // turn the pauseMenuUI off so it is invisible and the player can resume the game, also reseting the timescale
    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        paused = false;
        pauseButton.SetActive(true);
    }
    // turn the pauseMenuUI on so it is visible and useable to the player, pausing the timescale in the game
    void Paused()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        paused = true;
        pauseButton.SetActive(false);
    }


    // when the button in game is clicked
    public void OnClickPause()
    {
        // if the game is paused resume, if not pause
        if (paused)
        {
            Resume();
        }
        else
        {
            Paused();
        }
    }



    public void OnClickLevelSelector()
    {
        // SceneManager.LoadLevel(LevelSelector)
    }

    public void OnClickOptions()
    {
        // loads separate UI of options in game
    }

    public void OnClickMenu()
    {
        // SceneManager.loadlevel(MainMenu)
    }
}
