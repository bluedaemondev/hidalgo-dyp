using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ClimateState
{
    RAIN,
    STORM,
    CLEAR
}

/// <summary>
/// Rain : se afecta a los objetos que tengan IScalableWWeather
/// Storm :  se activa un contador para los pasos del player que va juntando estatica
/// Clear : momento en el que para de llover pero se siguen escuchando truenos / escena de sigilo
/// </summary>

public class ClimateController : MonoBehaviour
{
    public static ClimateController instance { get; private set; }

    [SerializeField] private ClimateState currentWeather;
    public UnityEvent<ClimateState> onChangeClimate;

    public ClimateState CurrentWeather
    {
        get => currentWeather;
        set
        {
            currentWeather = value;
            if (onChangeClimate != null)
                onChangeClimate.Invoke(currentWeather);
        }
    }

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;

        onChangeClimate = new UnityEvent<ClimateState>();
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        this.CurrentWeather = ClimateState.RAIN;
    }
}
