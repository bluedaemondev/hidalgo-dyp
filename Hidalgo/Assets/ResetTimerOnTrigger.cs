using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTimerOnTrigger : MonoBehaviour
{
    public float addTimeParameter = 15f;
    public bool useAddMax = false;
    public LayerMask interactWith;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Common.GetLayersFromMask(interactWith).Contains(collision.gameObject.layer))
            return;

        PickupsScapeGameManager.instance.AddMaxTime(useAddMax ? 0 : addTimeParameter);


    }
}
