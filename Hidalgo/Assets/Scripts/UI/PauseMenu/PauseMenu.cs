using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    
    /// <summary>
    /// Pendientes de cambio - gonza 27-4
    /// </summary>
    //public GameObject Timer;
    //public CounterController CounterState;

    public GameObject PauseMenuUI;

    //private void Awake()
    //{
    //    //Timer.GetComponent<CounterController>();
    //}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void ResumeGame()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        //CounterState.ContinueTimer();
    }

    void PauseGame()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        //CounterState.PauseTimer();
    }
}


