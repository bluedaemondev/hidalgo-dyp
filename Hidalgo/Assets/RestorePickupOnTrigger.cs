using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestorePickupOnTrigger : MonoBehaviour
{
    public LayerMask interactsWith;

    public PickupController controller;
    public GameObject prefabRestore;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Common.GetLayersFromMask(interactsWith).Contains(collision.gameObject.layer) || !controller.isOut)
            return;

        Debug.Log("Restoring pickup");
        controller.ResetPickupComponents(true);

        StartCoroutine(ResetPosition());

    }
    IEnumerator ResetPosition()
    {
        EffectFactory.instance.InstantiateEffectAt(prefabRestore, transform.position, Quaternion.identity, transform);
        while ((Vector2)transform.position != controller.originalPosition)
        {
            var movementDir = Vector2.Lerp(transform.position, controller.originalPosition, Time.deltaTime * 2);
            transform.position = movementDir;
            yield return null;
        }

    }
}
