using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleController : MonoBehaviour
{
    public Transform positionKnockbackTarget;
    public float forceApply;

    public LayerMask interactsWith;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Common.GetLayersFromMask(interactsWith).Contains(collision.gameObject.layer))
            return;

        var stunneable = collision.GetComponent<IStunneable>();
        if(stunneable != null)
        {
            //lightningGo.GetComponent<Lightning>()
            ClimateController.instance.LightningEffect(collision.transform.position);

            var force = stunneable.GetRigidbody().transform.position - transform.position;
            force.Normalize();

            stunneable.GetStunned(0.75f);
            stunneable.GetRigidbody().AddForce(force * forceApply, ForceMode2D.Impulse);

        }


    }

}
