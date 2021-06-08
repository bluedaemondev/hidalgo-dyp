using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightManager : MonoBehaviour
{
    public static LightManager instance { get; private set; }

    [SerializeField, Header("intensidad maxima de luz en dia")]
    float lightDaytime = 4;
    [SerializeField] float timePass = 0.2f;

    public float lightningFlashDuration;
    public float flashIntensity = 10;

    public Light2D globalLightScene;
    private float originalLightIntensity;

    Coroutine lightningCoroutine;


    public void Lightning()
    {
        lightningCoroutine = StartCoroutine(LightningFlash());
    }

    private void Update()
    {
        if(lightningCoroutine == null && globalLightScene.intensity != originalLightIntensity)
        {
            globalLightScene.intensity = originalLightIntensity;
        }
    }
    IEnumerator LightningFlash()
    {
        var originalIntensityGlobal = this.globalLightScene.intensity;

        this.globalLightScene.intensity = flashIntensity;
        SoundManager.instance.PlayAmbient(PickupsScapeGameManager.instance.soundLibrary.lightning);
        yield return new WaitForSeconds(lightningFlashDuration);
        this.globalLightScene.intensity = originalIntensityGlobal;
    }
    IEnumerator TimeToDaytime()
    {
        while (Time.time < 120 && globalLightScene.intensity < lightDaytime)
        {
            this.globalLightScene.intensity = Mathf.Lerp(this.globalLightScene.intensity, this.lightDaytime, Time.deltaTime * this.timePass / 100);
            yield return null;
        }
    }
    private void Awake()
    {
        if (instance != this)
        {
            Destroy(instance);
        }

        instance = this;
    }
    private void Start()
    {
        originalLightIntensity = globalLightScene.intensity;
        StartCoroutine(TimeToDaytime());
    }
}
