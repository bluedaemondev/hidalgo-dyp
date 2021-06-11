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

    public Transform golpePoint;
    public float golpeRange = 0.5f;
    public LayerMask enemyLayers;

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
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(golpePoint.position, golpeRange, enemyLayers);

        //Damage enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("Le dimos a" + enemy.name);
        }
    }

    //Gizmos for Golpe point and range
    void OnDrawGizmosSelected()
    {
        if (golpePoint == null)
            return;

        Gizmos.DrawWireSphere(golpePoint.position, golpeRange);
    }
}

