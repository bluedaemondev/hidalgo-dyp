using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserEnemyM2 : EnemyM2
{
    public float velocityMultiplierWithPickup = 1.5f;
    public SpriteRenderer spriteRendHandPickup;

    public string animation_WalkBool = "walking";
    public string animation_hasPickupBool = "hasPickup";

    public string animation_damagedTrigger = "damaged";
    public string animation_knockedOutTrigger = "knocked";

    public PickupController pickupInHand = null;

    public Vector2 positionNext;

    private Action ArtificialFixedUpdate;

    public BoxCollider2D interactionWithPickup;
    public float timeResetPickupBox = 1.5f;

    public void SetHandPickup(PickupController pickup)
    {
        _animator.SetBool(animation_hasPickupBool, true);
        pickupInHand = pickup;
        pickupInHand.transform.parent = transform;

        spriteRendHandPickup.sprite = pickup.spriteAsset;

    }
    public void DropHandPickup()
    {
        if (pickupInHand != null)
        {
            _animator.SetBool(animation_hasPickupBool, false);
            pickupInHand.ResetPickupComponents();
            StartCoroutine(DisablePickupFor(timeResetPickupBox));

            PickupTracker.instance.CallbackDropped(pickupInHand);
            pickupInHand = null;

        }
    }

    private IEnumerator DisablePickupFor(float time)
    {
        SetExitTarget();
        interactionWithPickup.enabled = false;
        yield return new WaitForSeconds(time);
        interactionWithPickup.enabled = true;
    }

    public void DestroyObject02()
    {
        Destroy(gameObject, 0.2f);
    }

    /// <summary>
    /// Estados del enemigo que agarra los pickups: 
    /// - caminando
    /// - agarrando pickup
    /// - caminando para salir del mapa
    /// - noqueado
    /// - desapareciendo
    /// - peleando/atacando
    /// </summary>


    /// <summary>
    /// Solo recibe acciones para hacer basado en animacion y ontriggerenter de los pickups
    /// al agarrar un objeto, para salir del mapa
    /// no tiene ataque?

    public override float TakeDamage(float value)
    {
        value = base.TakeDamage(value);
        _animator.SetTrigger(animation_damagedTrigger);

        if (currentHealth <= 0)
        {
            KnockedOut();
        }

        return value;
    }

    /// <summary>
    /// Llamado todos los frames desde el animator, guarda la posicion siguiente para mover en fixed update
    /// </summary>
    public void MoveTowardsTargetPosition()
    {
        var direction = (targetPosition - (Vector2)transform.position).normalized;
        positionNext = direction * Time.deltaTime * movementSpeed + (Vector2)transform.position;
    }

    private void FixedUpdate()
    {
        ArtificialFixedUpdate();
    }

    public void CompareDroppedPickupToMyTarget(Vector2 positionDrop)
    {
        if (pickupInHand != null)
            return;

        float currentDistanceToTarget = Vector2.Distance(transform.position, targetPosition);
        float distanceToDrop = Vector2.Distance(transform.position, positionDrop);

        if(distanceToDrop <= currentDistanceToTarget)
        {
            SetPickupTarget(positionDrop);
        }
    }

    public void SetPickupTarget(Vector2 positionToReach)
    {
        _animator.SetBool(animation_WalkBool, true);

        this.SetTarget(positionToReach);
    }
    public void SetExitTarget()
    {
        _animator.SetBool(animation_WalkBool, true);
        //_animator.SetBool(animation_hasPickupBool, true);

        this.SetTarget(originalPosition);
        this.movementSpeed = this.movementSpeed * this.velocityMultiplierWithPickup;
    }
    private void Move()
    {
        this._rigidbody.MovePosition(positionNext);
    }
    private void KnockedOut()
    {
        ArtificialFixedUpdate = delegate { };

        Debug.Log("Enemy knocked out");
        _animator.SetTrigger(animation_knockedOutTrigger);

        DropHandPickup();

        //Disable enemy (body stays there for now)
        GetComponent<Collider2D>().enabled = false;
        
        //this.enabled = false;
    }
    private void Start()
    {
        PickupTracker.instance.onPickupDropped += CompareDroppedPickupToMyTarget;
    }
    private void OnDestroy()
    {
        PickupTracker.instance.onPickupDropped -= CompareDroppedPickupToMyTarget;
    }
    public override void Init()
    {
        _animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        health.Init(maxHealth, currentHealth);

        _rigidbody = GetComponent<Rigidbody2D>();

        originalPosition = transform.position;

        ArtificialFixedUpdate = Move;
    }

}
