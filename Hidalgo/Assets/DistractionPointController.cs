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
    public Rocinante rocinante;

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
        if (Common.GetLayersFromMask(interactsWith).Contains(collision.gameObject.layer))
        {
            DeactivateInteractionQTE();

            rocinante.Feed(this.myRigidbody);
            StartCoroutine(Cooldown());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Common.GetLayersFromMask(interactsWith).Contains(collision.gameObject.layer))
        {
            Debug.Log("collision " + collision.gameObject);
            interactionQTE.SetActive(true);
            interactionQTE.GetComponent<InteractionWithPlayerQTE>().ResetInteraction();
        }
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

    private void Start()
    {
        rocinante.onSpringTargetChanged += SetActiveBasedOnSpringState;
        DeactivateInteractionQTE();
    }

    void SetActiveBasedOnSpringState()
    {
        if (rocinante.Spring.connectedBody == PickupsScapeGameManager.instance.Player._rigidbody2D)
        {
            // el player tiene el caballo, entonces se activa la interaccion
            //ActivateInteractionQTE();
            //StartCoroutine(Cooldown());

        }
        else
        {
            this.CanDistract = false;
            this.GetComponent<Collider2D>().enabled = false;
            this.DeactivateInteractionQTE();

        }
    }
}
