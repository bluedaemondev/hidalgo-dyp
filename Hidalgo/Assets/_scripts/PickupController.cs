using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    [SerializeField] private string id;
    [SerializeField] private bool destroyOnPickup = true;
    public string Id { get => id; }

    public LayerMask interactsWith;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //PickupsScapeGameManager.instance.onPickupItem.Invoke(this.id);
        if (!Common.GetLayersFromMask(interactsWith).Contains(collision.gameObject.layer))
            return;

        if (collision.GetComponent<ChaserEnemyM2>() != null)
        {
            var chaser = collision.GetComponent<ChaserEnemyM2>();
            PickupTracker.instance.SetPickupMissing(gameObject.name);

            chaser.SetHandPickup(transform.GetChild(0).GetComponent<SpriteRenderer>().sprite);

        }

        if (destroyOnPickup)
        {    //Destroy(this.gameObject);
            Debug.Log("destroying pickup " + this.gameObject.name);
        }
    }


}
