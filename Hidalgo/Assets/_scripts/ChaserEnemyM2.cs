using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserEnemyM2 : EnemyM2
{
    [SerializeField]
    Transform targetPickup;
    
    public Transform pickupTransform;

    public Func<Vector2> moveTowardsTarget;

    public string animation_AttackName = "attacking";
    public string animation_WalkBool = "walking";
    public string animation_pickingUpName = "pickup";
    public string animation_damagedName = "damaged";
    public string animation_knockedOutTrigger = "knocked_out";

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
        _animator.Play(animation_damagedName);

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

        _pathfinder.onStopMovingCallback = SetExitTarget;
    }
    public void SetExitTarget()
    {
        _animator.SetBool(animation_WalkBool, true);
        this.SetTarget(originalPosition);

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
