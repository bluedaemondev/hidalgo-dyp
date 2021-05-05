using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;

public class TimerCountdown : MonoBehaviour
{
    public GameObject   timerDisplay;
    public int          secondsToReturnTo;
    public int          secondsLeft = 30;
    public bool         takingAway = false;

    private void Start()
    {
        timerDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
    }

    private void Update()
    {
        if (!takingAway && secondsLeft > 0)
        {
            StartCoroutine(TimerTake());
        }
    }
    IEnumerator TimerTake()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        if (secondsLeft < 10)
        {
           timerDisplay.GetComponent<Text>().text = "00:0" + secondsLeft;
        }
        else
        {
            timerDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
        }      
        takingAway = false;
    }

    public void ResetTimer()
    {
        secondsLeft = secondsToReturnTo;
    }




}
