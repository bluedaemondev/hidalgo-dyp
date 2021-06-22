using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HorseUIContainer : MonoBehaviour
{
    [SerializeField] Image img;

    [SerializeField] private Color colorMissing;
    [SerializeField] private Color colorActive = Color.white;

    private void Start()
    {
        SwitchState(false);
    }

    /// <summary>
    /// Cambia de color el ui dependiendo si tenes a Rocinante
    /// </summary>
    /// <param name="enabled"></param>
    public void SwitchState(bool enabled)
    {
        if (enabled)
            img.color = colorActive;
        else
            img.color = colorMissing;
    }

}
