using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_M2 : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;

    [SerializeField]
    Transform targetPickup;
    [SerializeField]
    private Vector2 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
        currentHealth = maxHealth;
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //Play Hurt anim

        if(currentHealth <= 0)
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
}
