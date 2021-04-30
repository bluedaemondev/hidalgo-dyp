using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manager de acciones en la escena para el nivel top down en el que llevas a 
/// rocinante y sancho hasta la meta
/// </summary>
public class PickupsScapeGameManager : MonoBehaviour
{
    public static PickupsScapeGameManager instance { get; private set; }
    public event Action onGameWin;
    public event Action onGameLose;

    public UnityEvent onPickupFuckingBullshit;

    public CounterController counter;
    private bool pickupsCompleted = false;

    public float timerMax = 60 * 3;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;

        Init();
    }
    private void Init()
    {
        onPickupFuckingBullshit = new UnityEvent();
        onGameWin += CloseApp;
        counter.StartTimerUpdateSeconds(timerMax,
            () => { onGameLose?.Invoke(); }, //salida
            (obj) => // cada segundo
            {
                FindObjectOfType<CronometerControllerUI>().SetText(obj.ToString());
            });

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
