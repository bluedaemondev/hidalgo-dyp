using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    private Collider2D collider2d;
    private Vector2 originalPosition;
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

            PickupTracker.instance.SetPickupMissing(gameObject.name);

            chaser.SetHandPickup(this);
            this.collider2d.enabled = false;
            this.spriteRenderer.enabled = false;
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

        this.transform.parent = originalParent;

        if (useOffset)
        {
            transform.position += (Vector3)Vector2.one * 6; 
        }
        if (resetPosition)
        {
            transform.position = this.originalPosition;
        }

    }
}
