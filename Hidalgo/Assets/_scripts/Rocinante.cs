using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocinante : MovementType
{
    [SerializeField] private float speed = 2;
    private float speedMultiplier = 1f;

    public float deltaMaxDist = 2;
    public float deltaMinToMove = 1.4f;
    private Transform _follows;

    public Transform Follows { get => _follows; set { _follows = value; Debug.Log("following = " + _follows); } }
    public float Speed { get => speed * speedMultiplier; }

    public void SetSpeedMultiplier(float value)
    {
        this.speedMultiplier = value;
    }
    public void ResetSpeedMultipliers()
    {
        this.speedMultiplier = 1;
    }

    public void Move()
    {

        var delta = Vector2.Distance(transform.position, Follows.position);

        if (delta <= deltaMaxDist && delta >= deltaMinToMove)
        {
            var direction = Vector2.MoveTowards(transform.position, Follows.position, Speed * Time.deltaTime);
            transform.position = direction;
        }
    }




}
