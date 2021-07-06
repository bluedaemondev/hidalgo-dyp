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

    public List<BoxCollider2D> interactionWithPickup;
    public float timeResetPickupBox = 1.5f;

    public bool canTakeDamage = true;

    public LayerMask layerEdificioSalida;

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

            pickupInHand.transform.rotation = Quaternion.identity;

            PickupTracker.instance.CallbackDropped(pickupInHand);
            pickupInHand = null;

        }
    }

    private IEnumerator DisablePickupFor(float time)
    {
        SetExitTarget();
        foreach (var item in interactionWithPickup)
        {
            item.enabled = false;
        }
        yield return new WaitForSeconds(time);
        foreach (var item in interactionWithPickup)
        {
            item.enabled = true;
        }
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

    private IEnumerator DeactivateHealth(float time)
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(time);
        _animator.ResetTrigger(animation_damagedTrigger);
        canTakeDamage = true;
    }
    public override float TakeDamage(float value)
    {

        if (!canTakeDamage)
            return 0;

        value = base.TakeDamage(value);
        //_animator.SetTrigger(animation_damagedTrigger);
        _animator.Play("Damaged");

        if (currentHealth <= 0)
        {
            KnockedOut();
        }

        //StartCoroutine(DeactivateHealth(1.2f));

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

        if (distanceToDrop <= currentDistanceToTarget)
        {
            SetPickupTarget(positionDrop);
        }
    }

    public void SetPickupTarget(Vector2 positionToReach)
    {
        Debug.Log(name + " total que puede ir mal = " + positionToReach);
        this.SetTarget(positionToReach);
        _animator.SetBool(animation_WalkBool, true);

    }
    public void SetExitTarget()
    {
        //Debug.Log(name + " total vuelta que puede ir mal = " + originalPosition);

        this.SetTarget(originalPosition);
        _animator.SetBool(animation_WalkBool, true);

        this.movementSpeed = this.movementSpeed * this.velocityMultiplierWithPickup;
    }

    bool wasActivePickup = false;
    private void Move()
    {
        this._rigidbody.MovePosition(positionNext);


        var dir = (targetPosition - (Vector2)transform.position).normalized;

        float angleRot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angleRot);

        //Vector2 raycastHyp = new Vector2(dir.y / Mathf.Asin(angleRot), dir.x / Mathf.Acos(angleRot)).normalized;
        //Debug.Log(raycastHyp * 10);


        var casted = Physics2D.Raycast(transform.position, angleRot * Vector2.up, 10, layerEdificioSalida);
        Debug.DrawRay(transform.position, angleRot * Vector2.up, Color.black, 1);

        if (casted.collider != null &&
        pickupInHand != null && !wasActivePickup)
        {
            Debug.Log("collision point " + casted.point + " - object = " + casted.collider.name);
            PickupTracker.instance.PickupLost(pickupInHand.gameObject);
            wasActivePickup = true;

            _animator.Play("Destroying");

            WaveSystem.instance.OnEnemyDied();
        }



    }
    private void KnockedOut()
    {
        this.Die();
        GetComponent<Collider2D>().enabled = false;

        ArtificialFixedUpdate = delegate { };

        //Debug.Log("Enemy knocked out");
        _animator.SetTrigger(animation_knockedOutTrigger);

        DropHandPickup();

        //Disable enemy (body stays there for now)

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
