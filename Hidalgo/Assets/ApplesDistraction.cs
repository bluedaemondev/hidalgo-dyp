using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplesDistraction : MonoBehaviour
{
    [SerializeField]
    private InteractionWithPlayerQTE interactionQTE;
    [SerializeField]
    private Rigidbody2D myRigidbody;
    private Collider2D aggroRange;

    public float cooldownAfterPassed = 4f;
    public Rocinante rocinante;
    public LayerMask rocinanteLayer;

    Coroutine cooldown;

    void Start()
    {
        if (rocinante == null)
            rocinante = FindObjectOfType<Rocinante>();
        if (myRigidbody == null)
            myRigidbody = GetComponent<Rigidbody2D>();
        
        if(interactionQTE == null)
        {
            interactionQTE = transform.GetComponentInChildren<InteractionWithPlayerQTE>();
        }

        this.aggroRange = GetComponent<Collider2D>();
        rocinante.onSpringTargetChanged += SetActiveBasedOnSpringState;
    }

    void SetActiveBasedOnSpringState()
    {
        Debug.Log("jkhashdk " + rocinante.IsAttachedToPlayer());

        if (rocinante.IsAttachedToPlayer())
        {
            // abro la posibilidad de disparar el QTE
            this.aggroRange.enabled = true;
            this.interactionQTE.ActivateRange();
        }
        else if (cooldown == null)
        {
            // solo le saco el aggro hasta que pase el QTE
            this.aggroRange.enabled = false;
        }
    }

    private IEnumerator Cooldown()
    {
        //while (rocinante.Spring.connectedBody == this.myRigidbody)
        //{
        this.aggroRange.enabled = false;
        //    yield return null;
        //}

        this.interactionQTE.DeactivateRange();

        yield return new WaitForSeconds(cooldownAfterPassed);

        this.aggroRange.enabled = true;
        this.interactionQTE.ActivateRange();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Common.GetLayersFromMask(rocinanteLayer).Contains(collision.gameObject.layer))
        {
            if (cooldown == null)
            {
                rocinante.Feed(this.myRigidbody);
                cooldown = StartCoroutine(Cooldown());
                rocinante.UncheckRocinante();
            }
        }
    }
}
