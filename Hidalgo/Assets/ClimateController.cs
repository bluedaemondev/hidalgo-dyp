using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ClimateState
{
    CLEAR,
    RAIN,
    STORM,
    THUNDERS
}

/// <summary>
/// Rain : se afecta a los objetos que tengan IScalableWWeather
/// Storm :  se activa un contador para los pasos del player que va juntando estatica
/// Clear : momento en el que para de llover pero se siguen escuchando truenos / escena de sigilo
/// </summary>

public class ClimateController : MonoBehaviour
{
    public static ClimateController instance { get; private set; }

    public GameObject prefabLightning;

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
        onChangeClimate.AddListener(SelectTracksMixer);
        this.CurrentWeather = ClimateState.CLEAR;
    }
    public void LightningEffect(Vector3 position)
    {
        LightManager.instance.Lightning();
        var tmp = Instantiate(prefabLightning, position, Quaternion.identity);

        //tmp.GetComponent<Animator>().SetTrigger("go");
    }
    private void SelectTracksMixer(ClimateState newState)
    {
        switch (newState) {
            case ClimateState.THUNDERS:
                SoundManager.instance.PlayAmbient(PickupsScapeGameManager.instance.soundLibrary.thunderAmbient);
                SoundManager.instance.PlayEffect(PickupsScapeGameManager.instance.soundLibrary.quijoteComplaining1);
                break;
            case ClimateState.RAIN:
                SoundManager.instance.PlayAmbient(PickupsScapeGameManager.instance.soundLibrary.rainAmbient);
                SoundManager.instance.PlayEffect(PickupsScapeGameManager.instance.soundLibrary.quijoteComplaining2);
                break;
            case ClimateState.STORM:
                SoundManager.instance.PlayAmbient(PickupsScapeGameManager.instance.soundLibrary.stormAmbient);
                SoundManager.instance.PlayEffect(PickupsScapeGameManager.instance.soundLibrary.quijoteComplaining1);
                //SoundManager.instance.PlayEffect(PickupsScapeGameManager.instance.soundLibrary.quijoteComplaining2);
                break;
        }
    }
}
