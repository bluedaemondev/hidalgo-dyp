using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Events;
using System;

public class InteractionWithPlayerQTE : MonoBehaviour
{
    [SerializeField]
    private Light2D lightInteraction;
    [SerializeField]
    private GameObject Distraccion;
    ApplesDistraction PrimerID;
    ApplesDistraction SegundoID;
    [SerializeField]
    private GameObject DistraccionUno;
    [SerializeField]
    private GameObject maskInteraction;
    [SerializeField]
    private GameObject extraRange;
    private Collider2D rangeStartEvent;
    private GameObject containerSprites;
    ApplesDistraction Distraction;

    private Vector2 originalScaleMask;
    public bool canTrigger = true;

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

    void Awake()
    {
      PrimerID =  Distraccion.GetComponent<ApplesDistraction>();
      SegundoID = DistraccionUno.GetComponent<ApplesDistraction>();
    }
    // Start is called before the first frame update
    void Start()
    {
        this.rangeStartEvent = GetComponent<Collider2D>();
        this.containerSprites = transform.GetChild(0).gameObject;

        this.originalScaleMask = maskInteraction.transform.localScale;
        //if (ResetsAfterAction)
        //    onPassed.AddListener(this.ResetInteraction);

        Debug.Log("VA 1 " + PrimerID.MyID);
        Debug.Log("VA 2 " + SegundoID.MyID);
        Debug.Log("SA 1 " + PrimerID.IDToSave);
        Debug.Log("SA 2 " + SegundoID.IDToSave);
    }
    
    public void DeleteAfterPassedRoutine()
    {
        Destroy(this.gameObject);
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
            Distraction = collision.GetComponent<ApplesDistraction>();
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

        if ((timeCurrent >= timeToTriggerQTE || tFactor >= 1) && canTrigger && Distraction.MyID == Distraction.IDToSave)
        {
            Debug.Log("TEHRE OYU GO");

            TriggerNewQTE();
            canTrigger = false;

            //if (ResetsAfterAction)
            //    ResetInteraction();
        }
    }
    public void TriggerNewQTE()
    {
        var tmpQte = Instantiate(QTEprefab, target.transform.position, Quaternion.identity, target.transform).GetComponent<QuickTimeEventController>();
        tmpQte.interactionBase = this;

    }
    public void ActivateRange()
    {
        this.rangeStartEvent.enabled = true;
        this.containerSprites.SetActive(true);
    }
    public void DeactivateRange()
    {
        this.rangeStartEvent.enabled = false;
        this.containerSprites.SetActive(false);
    }
}
