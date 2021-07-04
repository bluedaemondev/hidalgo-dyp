using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : MonoBehaviour
{
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
        
        isWalking = true;
        Attacked = true;
        des = player.transform.position;
        myAnimator = GetComponent<Animator>();
    }


    void Update()
    {
        if (isWalking)
        {
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
        yield return new WaitForSeconds(waitTime);
        isWalking = false;
    }

    

    
}
