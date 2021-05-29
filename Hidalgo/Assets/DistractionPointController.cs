using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class DistractionPointController : MonoBehaviour
{
    [SerializeField]
    private GameObject interactionQTE;

    [SerializeField]
    private Rigidbody2D myRigidbody;

    public float cooldownAfterPassed = 4f;
    private bool canDistract = true;
    //[SerializeField]
    public LayerMask interactsWith;

    public bool CanDistract
    {
        get => canDistract;
        private set
        {
            canDistract = value;
            interactionQTE.SetActive(canDistract);
        }
    }

    public void ActivateInteractionQTE()
    {
        CanDistract = true;
    }
    public void DeactivateInteractionQTE()
    {
        CanDistract = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Common.GetLayersFromMask(interactsWith).Contains(collision.gameObject.layer) && canDistract)
        {
            ActivateInteractionQTE();
            var rocinante = collision.gameObject.GetComponent<Rocinante>();
            if (rocinante != null)
            {
                rocinante.Feed(this.myRigidbody);
                StartCoroutine(Cooldown());
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //StopAllCoroutines();
        interactionQTE.SetActive(true);
        interactionQTE.GetComponent<InteractionWithPlayerQTE>().ResetInteraction();
        //canDistract = true;
    }

    IEnumerator Cooldown()
    {
        this.CanDistract = false;
        this.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(cooldownAfterPassed);
        this.CanDistract = true;
        this.GetComponent<Collider2D>().enabled = true;
    }
}
