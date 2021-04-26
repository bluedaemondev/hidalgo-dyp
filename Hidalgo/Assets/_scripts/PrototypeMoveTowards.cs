using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeMoveTowards : MonoBehaviour, IMoveType
{
    public float speed = 2;
    public float deltaMaxDist = 2;
    public float deltaMinToMove = 1.4f;
    private Transform _follows;

    public Transform Follows { get => _follows; set => _follows = value; }

    public void Move()
    {
        var delta = Vector2.Distance(transform.position, Follows.position);

        if (delta <= deltaMaxDist && delta >= deltaMinToMove)
            transform.position = Vector2.MoveTowards(transform.position, Follows.position, speed * Time.deltaTime);
    }

}
