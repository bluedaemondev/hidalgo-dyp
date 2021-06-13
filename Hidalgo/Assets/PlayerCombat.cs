using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    

    private bool isAttacking;
    private float attackTimer;

    public Transform golpePoint;
    public float golpeRange = 0.5f;
    public LayerMask enemyLayers;

    public Animator _animator;
    public string animation_AttackName = "attacking";
    public string animation_WalkName = "walking";
    public string animation_IdleName = "idle";
    public string animation_pickingUpName = "pickup";

    Collider2D[] hitEnemies;

    public int golpeDamage = 40;

    void Start()
    {
        _animator = GetComponent<Animator>();
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
                this._animator.Play(animation_IdleName);
            }
        }
    }

    void Golpe()
    {
        //Change to Golpe sprite
        this.isAttacking = true;
        this._animator.Play(animation_AttackName);

        //Detect enemies in range of Golpe
        hitEnemies = Physics2D.OverlapCircleAll(golpePoint.position, golpeRange, enemyLayers);

        //Damage enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy_M2>().TakeDamage(golpeDamage);
        }
    }

    //Gizmos for Golpe point and range
    void OnDrawGizmosSelected()
    {
        if (golpePoint == null)
            return;

        Gizmos.DrawWireSphere(golpePoint.position, golpeRange);
        if(hitEnemies != null)
        {
            foreach(var item in hitEnemies)
            {
                Gizmos.DrawWireCube(item.transform.position, Vector3.one);
            }
        }
    }
}

