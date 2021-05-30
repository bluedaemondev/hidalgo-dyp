using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBox : MonoBehaviour
{
    public AudioClip onPickup;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<Player>().InitBoxControls();
        SoundManager.instance.PlayEffect(onPickup);
        Destroy(this.gameObject);
    }
}
