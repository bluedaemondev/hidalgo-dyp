using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightManager : MonoBehaviour
{
    public static LightManager instance { get; private set; }

    public float lightningFlashDuration;
    public float flashIntensity = 10;

    public Light2D globalLightScene;

    public void Lightning()
    {
        StartCoroutine(LightningFlash());
    }
    IEnumerator LightningFlash()
    {
        var originalIntensityGlobal = this.globalLightScene.intensity;

        this.globalLightScene.intensity = flashIntensity;
        SoundManager.instance.PlayAmbient(PickupsScapeGameManager.instance.soundLibrary.lightning);
        yield return new WaitForSeconds(lightningFlashDuration);
        this.globalLightScene.intensity = originalIntensityGlobal;
    }
    private void Awake()
    {
        if(instance != this)
        {
            Destroy(instance);
        }

        instance = this;
    }
}
