using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Sprite golpeSprite;
    public Sprite idleSprite;
    private SpriteRenderer spriteRenderer;
    private bool isAttacking;
    private float attackTimer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Golpe();
        }

        //Change sprite back to idle sprite after Golpe
        if (this.isAttacking)
        {
            this.attackTimer += Time.deltaTime;
            if (this.attackTimer >= 0.5f)
            {
                this.isAttacking = false;
                this.attackTimer = 0f;
                this.spriteRenderer.sprite = idleSprite;
            }
        }
    }

    void Golpe()
    {
        //Change to Golpe sprite
        this.isAttacking = true;
        this.spriteRenderer.sprite = golpeSprite;

        
        //Detect enemies in range of Golpe
        //Damage enemies
    }
}

