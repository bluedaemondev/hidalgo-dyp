using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : MonoBehaviour
{
    private Health health;
    private Rigidbody2D myRigidBody;
    [SerializeField]
    private int maxHealth = 1;
    [SerializeField]
    private float speed = 3f;
    Vector3 des;
    Animator myAnimator;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject projectile;
    private bool isWalking;
    private bool Attacked;
    [SerializeField]
    private float waitTime = 3;
    [SerializeField]
    private float waitTimeToAttack = 3;
    bool isDone = false;


    void Start()
    {
        health = GetComponent<Health>();
        isWalking = true;
        Attacked = true;
        des = player.transform.position;
        myAnimator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        health.Init(maxHealth, maxHealth);
    }


    void Update()
    {

        /*Vector3 direction = Point.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        myRigidBody.rotation = angle;*/

        RotateTowards(des);
        //RotateTowards(Point.transform.position);

        if (isWalking)
        {
            SetEnemyState(1);
            transform.position = Vector3.MoveTowards(transform.position, des, speed * Time.deltaTime);
            StartCoroutine(WalkingTime());
        }
        else if (!isDone)
        {
            //Attack();
            Instantiate(projectile, transform.position, Quaternion.identity);
            //InvokeRepeating("Attack", waitTimeForAttack, waitTime);
            isDone = true;
            StartCoroutine(WaitToAttack());
        }


        //Debug.Log("eto que " + Attacked);
    }

    IEnumerator WaitToAttack()
    {

        yield return new WaitForSeconds(waitTimeToAttack);
        isDone = false;
    }

    IEnumerator WalkingTime()
    {
        SetEnemyState(2);
        yield return new WaitForSeconds(waitTime);
        isWalking = false;
    }

    private void RotateTowards(Vector2 target)
    {
        var offset = 90f;
        Vector2 direction = target - (Vector2)transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }

    public void SetEnemyState(float state)
    {
        if (myAnimator.GetFloat("RangeEnemyState") != state)
        {
            myAnimator.SetFloat("RangeEnemyState", state);
        }
    }

}

