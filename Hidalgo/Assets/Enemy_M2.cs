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

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;
        currentHealth = maxHealth;

    }
    private void Update()
    {
        this.movementDir = moveTowardsTarget();

        if (this.movementDir == positionNext)
        {
            this.moveTowardsTarget = MoveTowardsExit;
        }
    }
    Vector2 MoveTowardsPickups()
    {
        return Vector2.Lerp(transform.position, positionNext, movementSpeed * Time.deltaTime);
    }
    Vector2 MoveTowardsExit()
    {
        return Vector2.Lerp(transform.position, originalPosition, movementSpeed * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        _rigidbody.MovePosition(movementDir);
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //Play Hurt anim

        if (currentHealth <= 0)
        {
            KnockedOut();
        }
    }

    void KnockedOut()
    {
        Debug.Log("Enemy knocked out");

        //Knocked out anim

        //Disable enemy (body stays there for now)
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

    public void SetFollowTarget(Vector2 positionToReach)
    {
        this.positionNext = positionToReach;
        this.moveTowardsTarget = MoveTowardsPickups;
    }
}
