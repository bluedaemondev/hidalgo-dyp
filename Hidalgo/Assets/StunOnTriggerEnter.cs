using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunOnTriggerEnter : MonoBehaviour
{
    public float stunTime;
    public bool canTrigger = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(canTrigger && collision.gameObject.GetComponent<IStunneable>() != null)
        {
            canTrigger = false;
            collision.gameObject.GetComponent<IStunneable>().GetStunned(stunTime);
        }
    }
}
