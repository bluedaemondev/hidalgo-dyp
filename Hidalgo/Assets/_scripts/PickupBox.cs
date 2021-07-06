using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBox : MonoBehaviour
{
    public AudioClip onPickup;
    public GameObject onPickupTutorial;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(onPickupTutorial, transform.position, Quaternion.identity, null);

        collision.GetComponent<Player>().InitBoxControls();
        SoundManager.instance.PlayEffect(onPickup);
        Destroy(this.gameObject);
    }
}
