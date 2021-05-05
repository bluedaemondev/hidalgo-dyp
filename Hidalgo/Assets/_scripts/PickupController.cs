using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    [SerializeField] private string id;

    public string Id { get => id; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PickupsScapeGameManager.instance.onPickupItem.Invoke(this.id);
        
        Destroy(this.gameObject);
    }

}
