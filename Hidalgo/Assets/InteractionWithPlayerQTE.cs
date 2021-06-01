using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Events;

public class InteractionWithPlayerQTE : MonoBehaviour
{
    [SerializeField]
    private Light2D lightInteraction;

    [SerializeField]
    private GameObject maskInteraction;
    [SerializeField]
    private GameObject extraRange;

    private Vector2 originalScaleMask;
    public bool canTrigger = true;
    public bool ResetsAfterAction = false;

    [Header("Tiempo que tengo que estar en rango hasta accion")]
    public float timeToTriggerQTE = 3f;
    private float timeCurrent = 0f;
    private float tFactor = 0f;
    public float resetTimecooldown = 6f;

    [Header("Variacion de teclas armada (prefab)")]
    public GameObject QTEprefab;
    private GameObject target;

    [SerializeField]
    private LayerMask interactsWith;

    public UnityEvent onPassed;
    public UnityEvent onFailed;

    // Start is called before the first frame update
    void Start()
    {
        this.originalScaleMask = maskInteraction.transform.localScale;
        if (ResetsAfterAction)
            onPassed.AddListener(this.ResetInteraction);
    }

    public void ResetInteraction()
    {
        StopAllCoroutines();

        if (this.gameObject.activeSelf)
            StartCoroutine(Cooldown());
    }
    IEnumerator Cooldown()
    {
        timeCurrent = 0;
        tFactor = 0;
        maskInteraction.transform.localScale = this.originalScaleMask;

        this.canTrigger = false;
        this.GetComponent<Collider2D>().enabled = false;
        extraRange.SetActive(false);

        yield return new WaitForSeconds(resetTimecooldown);

        extraRange.SetActive(true);
        this.GetComponent<Collider2D>().enabled = true;
        this.canTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Common.GetLayersFromMask(interactsWith).Contains(collision.gameObject.layer) && canTrigger)
        {
            target = collision.gameObject;
            extraRange.SetActive(false);
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Common.GetLayersFromMask(interactsWith).Contains(collision.gameObject.layer) && canTrigger)
        {
            ComputePlayerInRange();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Common.GetLayersFromMask(interactsWith).Contains(collision.gameObject.layer))
        {
            ResetInteraction();

        }
    }
    void ComputePlayerInRange()
    {


        this.timeCurrent += Time.deltaTime;
        if (tFactor < 1)
        {
            tFactor += Time.deltaTime / timeToTriggerQTE;
            maskInteraction.transform.localScale = Vector3.Lerp(originalScaleMask, Vector3.zero, tFactor);
        }

        if ((timeCurrent >= timeToTriggerQTE || tFactor >= 1) && canTrigger)
        {
            TriggerNewQTE();
            canTrigger = false;

            if (ResetsAfterAction)
                ResetInteraction();
            //else
            //    StartCoroutine(Cooldown());
        }
    }
    public void TriggerNewQTE()
    {
        var tmpQte = Instantiate(QTEprefab, target.transform.position, Quaternion.identity, target.transform).GetComponent<QuickTimeEventController>();
        tmpQte.interactionBase = this;

    }
}
