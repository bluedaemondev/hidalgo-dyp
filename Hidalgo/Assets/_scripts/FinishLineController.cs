using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineController : MonoBehaviour
{
    public LayerMask layersToInteract;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!Common.GetLayersFromMask(layersToInteract).Contains(collision.gameObject.layer))
            return;

        PickupsScapeGameManager.instance.Win();

    }

}
