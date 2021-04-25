using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeMoveTowards : MonoBehaviour
{
    public float speed = 2;
    public float deltaMaxDist = 2;
    public float deltaMinToMove = 1.4f;
    private Transform follows;
    public void SetTargetToFollow(Transform t)
    {
        follows = t;
    }
    public void MoveTowardsTarget()
    {
        var delta = Vector2.Distance(transform.position, follows.position);

        if (delta <= deltaMaxDist && delta >= deltaMinToMove)
            transform.position = Vector2.MoveTowards(transform.position, follows.position, speed * Time.deltaTime);

        //Debug.Log(this.gameObject.name + " is moving");
    }
}
