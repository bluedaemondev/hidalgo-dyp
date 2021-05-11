using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    [SerializeField] private string id;
    [SerializeField] private bool destroyOnPickup = true;
    public string Id { get => id; }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        PickupsScapeGameManager.instance.onPickupItem.Invoke(this.id);

        if (destroyOnPickup)
            Destroy(this.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PickupsScapeGameManager.instance.onPickupItem.Invoke(this.id);
    }

}
