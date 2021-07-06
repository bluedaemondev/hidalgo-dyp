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
    public AudioClip audioHit;
    public List<AudioClip> audioDeath;

    private void Start()
    {
        health = healthMax;
    }

    public int GetMaxLife()
    {
        return this.healthMax;
    }
    public float GetHealth()
    {
        return (float)health;
    }

    public float GetHealthNormalized()
    {
        return (float)health / healthMax;
    }

    public void Init(int maxHealth, int currentHealth)
    {
        this.healthMax = maxHealth;
        this.health = currentHealth;
    }

    public void Damage(int amount)
    {
        health -= amount;

        if (this.audioHit != null)
            SoundManager.instance.PlayEffect(this.audioHit);


        if (health < 0)
        {
            health = 0;

            
        }

        OnHealthChanged?.Invoke(this, EventArgs.Empty);
        OnDamaged?.Invoke(this, EventArgs.Empty);

        if (bloodPrefab != null)
            EffectFactory.instance.InstantiateEffectAt(bloodPrefab, transform.position, Quaternion.identity);

        EffectFactory.instance.camShake.ShakeCameraNormal(8, 0.3f);

        if (health <= 0)
        {
            Die();
            if (this.audioHit != null)
            {
                int randDeath = UnityEngine.Random.Range(0, audioDeath.Count);
                SoundManager.instance.PlayEffect(this.audioDeath[randDeath]);
            }
            this.enabled = false;
        }
    }

    public void Die()
    {
        EffectFactory.instance.camShake.ShakeCameraNormal(2, 2f);

        OnDead?.Invoke(this, EventArgs.Empty);
        //SendMessage("OnDie");

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
