using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    
    /// <summary>
    /// Pendientes de cambio - gonza 27-4
    /// </summary>
    //public GameObject Timer;
    //public CounterController CounterState;

    public GameObject PauseMenuUI;
    public CounterController CounterState;

    //private void Awake()
    //{
    //    //Timer.GetComponent<CounterController>();
    //}

    private void Start()
    {
     CounterState = GameObject.FindObjectOfType<CounterController>();
    }
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
    public void RetryScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ResumeGame()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        CounterState.ContinueTimer();
        //CounterState.ContinueTimer();
    }

    void PauseGame()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        CounterState.PauseTimer();
        //CounterState.PauseTimer();
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
}


