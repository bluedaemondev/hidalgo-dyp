using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    public bool isOut = false;

    private Collider2D collider2d;
    public Vector2 originalPosition;
    private SpriteRenderer spriteRenderer;
    public Sprite spriteAsset;

    private Transform originalParent;

    [SerializeField] private string id;
    [SerializeField] private bool destroyOnPickup = false;
    public string Id { get => id; }

    public LayerMask interactsWith;

    private void Start()
    {
        this.originalParent = transform.parent;

        this.spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        this.collider2d = GetComponent<Collider2D>();

        this.spriteAsset = spriteRenderer.sprite;
        this.originalPosition = transform.position;

        //Debug.Log("pickup  - " + this.name + " " + this.originalPosition);

        // coleccion para sprrend,coll,position
        this.ResetPickupComponents();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //PickupsScapeGameManager.instance.onPickupItem.Invoke(this.id);
        if (!Common.GetLayersFromMask(interactsWith).Contains(collision.gameObject.layer))
            return;

        if (collision.GetComponent<ChaserEnemyM2>() != null)
        {
            var chaser = collision.GetComponent<ChaserEnemyM2>();

            //PickupTracker.instance.mis(gameObject.name);

            this.isOut = true;

            chaser.SetHandPickup(this);
            this.collider2d.enabled = false;
            this.spriteRenderer.enabled = false;

            Debug.Log("setting out ontriggerenter");
            chaser.SetExitTarget();
        }

        if (destroyOnPickup)
        {
            Debug.Log("destroying pickup " + this.gameObject.name);
            Destroy(gameObject);
        }
    }

    public void ResetPickupComponents(bool resetPosition = false, bool useOffset = false)
    {
        collider2d.enabled = true;
        spriteRenderer.enabled = true;

        if (useOffset)
        {
            transform.position += (Vector3)Vector2.one * 6; 
        }
        if (resetPosition)
        {
            Debug.Log(this.transform.position);
            Debug.Log("pickup  - " + this.name + " " + this.originalPosition);

            isOut = false;
        }

        this.transform.parent = originalParent;


    }
}
