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

    Coroutine cooldown;

    void Start()
    {
        this.aggroRange = GetComponent<Collider2D>();
        rocinante.onSpringTargetChanged += SetActiveBasedOnSpringState;
    }

    void SetActiveBasedOnSpringState()
    {
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
        this.aggroRange.enabled = false;
        this.interactionQTE.DeactivateRange();

        yield return new WaitForSeconds(cooldownAfterPassed);

        this.aggroRange.enabled = true;
        this.interactionQTE.ActivateRange();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == rocinante.gameObject)
        {
            if (cooldown == null)
            {
                rocinante.Feed(this.myRigidbody);
                cooldown = StartCoroutine(Cooldown());
            }
        }
    }
}
