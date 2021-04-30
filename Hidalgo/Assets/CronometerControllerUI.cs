using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Text = TMPro.TextMeshProUGUI;

public class CronometerControllerUI : MonoBehaviour
{
    private Text textContainer;
    public void SetText(string newText)
    {
        textContainer.text = newText;
    }

    private void Awake()
    {
        textContainer = GetComponent<Text>();
    }
}
