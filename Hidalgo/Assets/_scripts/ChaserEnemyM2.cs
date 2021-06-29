using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserEnemyM2 : EnemyM2
{
    public float velocityMultiplierWithPickup = 1.5f;
    public SpriteRenderer spriteRendHandPickupPlaceholder;


    public string animation_WalkBool = "walking";
    public string animation_hasPickupBool = "hasPickup";

    public string animation_damagedTrigger = "damaged";
    public string animation_knockedOutTrigger = "knocked";

    public void SetHandPickup(Sprite sprite)
    {
        spriteRendHandPickupPlaceholder.sprite = sprite;
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

    public void SetPickupTarget(Vector2 positionToReach)
    {
        _animator.SetBool(animation_WalkBool, true);
        this.SetTarget(positionToReach);
        _pathfinder.speedMultiplier = 1;

        _pathfinder.onStopMovingCallback = () => { Debug.Log("callback pickup object"); };
        // probando desde animacion
        //_pathfinder.onStopMovingCallback = SetExitTarget;
    }
    public void SetExitTarget()
    {
        _animator.SetBool(animation_WalkBool, true);
        _animator.SetBool(animation_hasPickupBool, true);

        this.SetTarget(originalPosition);
        _pathfinder.speedMultiplier = velocityMultiplierWithPickup;

        _pathfinder.onStopMovingCallback = () => { Debug.Log("callback exit map"); };
    }

    private void KnockedOut()
    {
        Debug.Log("Enemy knocked out");
        _animator.SetTrigger(animation_knockedOutTrigger);
        
        //Disable enemy (body stays there for now)
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
    public override void Init()
    {
        _animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        health.Init(maxHealth, currentHealth);

        _rigidbody = GetComponent<Rigidbody2D>();

        if (!_pathfinder)
            _pathfinder = GetComponent<CharacterPathfindingMovementHandler>();

        originalPosition = transform.position;
    }

}
