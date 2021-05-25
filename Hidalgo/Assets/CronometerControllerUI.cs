using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Text = TMPro.TextMeshProUGUI;

public class CronometerControllerUI : MonoBehaviour
{
    private Text textContainer;
    public float time;

    public void SetText(string newText)
    {
        textContainer.text = newText;
    }

    private void Awake()
    {
        textContainer = GetComponent<Text>();
    }

    public void OnAddTime(float time)
    {
        Debug.Log(this.textContainer.text);
        this.time = time;
        this.SetText(time.ToString());

        if (this.time >= PickupsScapeGameManager.TIMER_MAX)
        {

        }
    }
}
