using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_M2 : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public float movementSpeed = 4f;

    [SerializeField]
    Transform targetPickup;
    [SerializeField]
    private Vector2 originalPosition;

    private Vector2 positionNext;
    private Vector2 movementDir;
    private Rigidbody2D _rigidbody;

    public Transform pickupTransform;

    public Func<Vector2> moveTowardsTarget;
    protected Health health;

    public Animator _animator;
    public string animation_AttackName = "attacking";
    public string animation_WalkBool = "walking";
    public string animation_IdleName = "idle";
    public string animation_pickingUpName = "pickup";
    public string animation_damagedName = "damaged";
    public string animation_knockedOutTrigger = "knocked_out";

    [SerializeField] CharacterPathfindingMovementHandler _pathfinder;

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


    void Start()
    {
        Init();
    }


    public void TakeDamage(int damage)
    {
        health.Damage(damage);
        _animator.Play(animation_damagedName);

        if (currentHealth <= 0)
        {
            KnockedOut();
        }
    }
    public void SetPickupTarget(Vector2 positionToReach)
    {
        _animator.SetBool(animation_WalkBool, true);
        _pathfinder.SetMovementPath(positionToReach);

        _pathfinder.onStopMovingCallback = SetExitTarget;
    }
    public void SetExitTarget()
    {
        _animator.SetBool(animation_WalkBool, true);
        _pathfinder.SetMovementPath(originalPosition);

        _pathfinder.onStopMovingCallback = () => { Debug.Log("callback"); };
    }


    private void KnockedOut()
    {
        Debug.Log("Enemy knocked out");
        _animator.SetTrigger(animation_knockedOutTrigger);
        
        //Disable enemy (body stays there for now)
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
    private void Init()
    {
        _animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        health.Init(maxHealth, currentHealth);

        _rigidbody = GetComponent<Rigidbody2D>();

        if (!_pathfinder)
            _pathfinder = GetComponent<CharacterPathfindingMovementHandler>();

        originalPosition = transform.position;
        //currentHealth = maxHealth;
    }

}
