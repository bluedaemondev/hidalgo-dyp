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

    private bool pickupsCompleted = false;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
            Destroy(instance);

        instance = this;

    }

    public void Win()
    {
        
        if (onGameWin != null && pickupsCompleted)
        {
            onGameWin();
        }
    }

    public void SetPickupsCompletedState()
    {
        pickupsCompleted = true;
    }
}
