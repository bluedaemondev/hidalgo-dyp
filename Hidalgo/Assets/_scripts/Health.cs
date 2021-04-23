using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event EventHandler OnHealthChanged;
    public event EventHandler OnDamaged;
    public event EventHandler OnHealed;
    public event EventHandler OnDead;

    private int healthMax = 100;
    private int health;

    public GameObject bloodPrefab;

    private void Start()
    {
        health = healthMax;


    }

    public int GetMaxLife()
    {
        return this.healthMax;
    }

    public float GetHealthNormalized()
    {
        return (float)health / healthMax;
    }

    public void Damage(int amount)
    {
        health -= amount;
        if (health < 0)
        {
            health = 0;
        }
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
        OnDamaged?.Invoke(this, EventArgs.Empty);

        EffectFactory.instance.InstantiateEffectAt(bloodPrefab, transform.position, Quaternion.identity);
        FindObjectOfType<CameraShake>().ShakeCameraNormal(8, 0.3f);

        if (health <= 0)
        {
            Die();
            FindObjectOfType<CameraShake>().ShakeCameraNormal(2, 2f);

        }
    }

    public void Die()
    {
        OnDead?.Invoke(this, EventArgs.Empty);
        SendMessage("OnDie");

    }

    public bool IsDead()
    {
        return health <= 0;
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > healthMax)
        {
            health = healthMax;
        }
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
        OnHealed?.Invoke(this, EventArgs.Empty);
    }
}
