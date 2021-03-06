using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private bool isAttacking;
    private float attackTimer;

    public Transform golpePoint;
    public float golpeRange = 0.5f;
    public float golpeRate = 1f;
    float nextGolpeTime = 0f;
    public LayerMask enemyLayers;

    public Animator _animator;
    public string animation_AttackName = "attacking";
    public string animation_WalkName = "walking";
    public string animation_IdleName = "idle";
    public string animation_pickingUpName = "pickup";
    public string animation_damagedName = "damaged";

    Collider2D[] hitEnemies;

    public int golpeDamage = 40;

    private System.Action AttackHandler;


    public AudioClip audioWhooshAttack;

    void Start()
    {
        _animator = GetComponent<Animator>();
        AttackHandler = Golpe;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AttackHandler();
        }

        ////Input for Golpe, limited by golpeRate
        //if (Time.time >= nextGolpeTime)
        //{
        //    if (Input.GetKeyDown(KeyCode.Mouse0))
        //    {
        //        Golpe();
        //        nextGolpeTime = Time.time + golpeRate / 2f;
        //    }
        //}


        ////Change sprite back to idle sprite after Golpe
        //if (this.isAttacking)
        //{
        //    this.attackTimer += Time.deltaTime;
        //    if (this.attackTimer >= 0.5f)
        //    {
        //        this.isAttacking = false;
        //        this.attackTimer = 0f;
        //        this._animator.Play(animation_IdleName);
        //    }
        //}
    }

    private IEnumerator CooldownAttack()
    {
        AttackHandler = delegate { };
        //this._animator.Play(animation_IdleName);

        yield return new WaitForSecondsRealtime(golpeRate);
        AttackHandler = Golpe;

    }

    void Golpe()
    {

        //Change to Golpe sprite
        this.isAttacking = true;
        this._animator.Play(animation_AttackName);
        SoundManager.instance.PlayEffect(audioWhooshAttack);
        EffectFactory.instance.camShake.ShakeCameraNormal(0.2f, 0.2f);

        //Detect enemies in range of Golpe
        hitEnemies = Physics2D.OverlapCircleAll(golpePoint.position, golpeRange, enemyLayers);

        //Damage enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyM2>().TakeDamage(golpeDamage);
        }

        StartCoroutine(CooldownAttack());

    }

    //Gizmos for Golpe point and range
    void OnDrawGizmosSelected()
    {
        if (golpePoint == null)
            return;

        Gizmos.DrawWireSphere(golpePoint.position, golpeRange);
        //if(hitEnemies != null)
        //{
        //    foreach(var item in hitEnemies)
        //    {
        //        Gizmos.DrawWireCube(item.transform.position, Vector3.one);
        //    }
        //}
    }
}

