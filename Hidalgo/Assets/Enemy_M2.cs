﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_M2 : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;

    void Start()
    {
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

        //Disable enemy
    }
}
