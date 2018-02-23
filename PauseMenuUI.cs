using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour {

    public static bool isGamePaused = false;
    public GameObject pauseMenuUI;
	
	// Update is called once per frame
	void Update () {
        if (!SceneManager.GetSceneByName("MainMenu").isLoaded)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isGamePaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
	}

    public void Resume()
    {
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    public void Pause()
    {
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;        //temp fix
        pauseMenuUI.SetActive(false);   //temp fix
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
