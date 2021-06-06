using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene_Victoria_Trigger : MonoBehaviour
{
    public GameObject Cutscene_V;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 15) //colisión contra trigger para cutscene en salida
        {
            Cutscene_V.SetActive(true); //play cutscene de victoria
        }
    }
}
