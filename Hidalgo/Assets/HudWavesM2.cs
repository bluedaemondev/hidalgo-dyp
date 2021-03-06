using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudWavesM2 : MonoBehaviour
{
    public static HudWavesM2 instance { get; private set; }

    public Slider sliderWave;
    public TMPro.TextMeshProUGUI textWave;
    public TMPro.TextMeshProUGUI enemiesRemaining;

    public GameObject prefabLoseUI;

    public string textWavePrec = "Oleada ";

    private int copyEnemiesRemaining;

    public void OnLose()
    {
        prefabLoseUI.SetActive(true);
        Time.timeScale = 0.14f;
    }

    public void OnNewWave(int waveNumber)
    {
        textWave.text = textWavePrec + waveNumber;
        int remaining = WaveSystem.instance.GetGroupRemainingTotal();
        copyEnemiesRemaining = remaining;

        enemiesRemaining.text = /*"Enemigos restantes " + */remaining.ToString();
        sliderWave.maxValue = remaining;
        sliderWave.value = sliderWave.maxValue;
    }
    public void OnEnemyKilled()
    {
        enemiesRemaining.text = /*"Enemigos restantes " + */(--copyEnemiesRemaining).ToString();
        sliderWave.value--;
    }

    // Update is called once per frame
    void Awake()
    {
        Time.timeScale = 1;
        instance = this;
    }
}
