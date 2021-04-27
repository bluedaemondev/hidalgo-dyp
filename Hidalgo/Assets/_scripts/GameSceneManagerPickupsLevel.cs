using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manager de acciones en la escena para el nivel top down en el que llevas a 
/// rocinante y sancho hasta la meta
/// </summary>
public class GameSceneManagerPickupsLevel : MonoBehaviour
{
    public static GameSceneManagerPickupsLevel instance { get; private set; }
    public event Action onGameWin;
    public event Action onGameLose;

    public UnityEvent onPickupFuckingBullshit;

    private bool pickupsCompleted = false;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
            Destroy(instance);

        instance = this;

        onPickupFuckingBullshit = new UnityEvent();

        onGameWin += CloseApp;
    }

    public void Win()
    {
        if (onGameWin != null && pickupsCompleted)
        {
            Debug.Log("se llama");
            onGameWin();
        }
    }

    public void SetPickupsCompletedState()
    {
        pickupsCompleted = true;
    }

    public void CloseApp()
    {
        Debug.Log("Ganaste rey felicitaciones por esta experiencia fantastica");
        Application.Quit();
    }
}
