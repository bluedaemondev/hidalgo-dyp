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

    public SoundLibrarySO soundLibrary;

    public UnityEvent<string> onPickupItem;

    public CounterController counter;
    private bool pickupsCompleted = false;

    public const float TIMER_MAX = 17f;
    private float currTimeMax = TIMER_MAX;
    
    private CronometerControllerUI cronom;

    [SerializeField] private PickupCounterUI pickupCounter;
    public GameObject winGO;


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
        onPickupItem = new UnityEvent<string>();
        cronom = FindObjectOfType<CronometerControllerUI>();
        pickupCounter = FindObjectOfType<PickupCounterUI>();

        this.onPickupItem.AddListener(CheckPickupsCompleted);
        this.onGameWin += EnableWinObj;

        //counter.StartTimerUpdateSeconds(currTimeMax,
        //    () => { onGameLose?.Invoke(); }, //salida
        //    (obj) => // cada segundo
        //    {
        //        cronom.SetText((cronom.time - obj).ToString());
        //    });

    }

    public void AddMaxTime(float tAdd)
    {
        // flag reservado, se usa para agregar un tiempo igual al maximo que esta definido en el componente
        // (si pongo en el editor que el contador es 2 minutos, va a agregar 2 minutos por cada pickup levantado.
        //  hay que usar un valor bajo y probar este metodo si sirve mejor asi o enviando x segundos)
        if(tAdd == 0)
        {
            cronom.OnAddTime(TIMER_MAX);
            this.currTimeMax = TIMER_MAX;
        }
        else
        {
            cronom.OnAddTime(tAdd);
            this.currTimeMax = cronom.time;

        }
        
    }
    void EnableWinObj()
    {
        this.winGO.SetActive(true);
    }
    public void Win()
    {
        if (onGameWin != null && pickupsCompleted)
        {
            Debug.Log("se llama");
            onGameWin();
        }
    }

    private void CheckPickupsCompleted(string pickedUp)
    {
        if (pickupCounter.IsCompleted())
        {
            pickupsCompleted = true;
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
