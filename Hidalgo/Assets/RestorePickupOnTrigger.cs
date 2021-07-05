using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestorePickupOnTrigger : MonoBehaviour
{
    public LayerMask interactsWith;

    public PickupController controller;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Common.GetLayersFromMask(interactsWith).Contains(collision.gameObject.layer) || !controller.isOut)
            return;

        Debug.Log("Restoring pickup");
        controller.ResetPickupComponents(true);

    }
}
