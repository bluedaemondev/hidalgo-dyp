using System;
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

    private Rigidbody2D _rigidbody2d;

    public event Action onSpringTargetChanged;

    private SpringJoint2D spring;
    public GameObject cuerda;




    public Transform Follows { get => _follows; set { _follows = value; Debug.Log("following = " + _follows); } }
    public float Speed { get => speed * speedMultiplier; }
    public SpringJoint2D Spring { get => spring; set { spring = value; onSpringTargetChanged?.Invoke(); } }

    private void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        Spring = GetComponent<SpringJoint2D>();
    }

    public void SetSpeedMultiplier(float value)
    {
        this.speedMultiplier = value;
    }
    public void ResetSpeedMultipliers()
    {
        this.speedMultiplier = 1;
    }

    public void FollowTarget(Transform targetNew)
    {
        Spring.gameObject.SetActive(true);
        Follows = targetNew;
        Spring.connectedBody = targetNew.GetComponent<Rigidbody2D>();
        Spring.autoConfigureDistance = false;

        cuerda.SetActive(true);
        Spring.distance = 3.5f;
        //cuerda.connectedAnchor = Vector2.zero;


    }
    public void StopFollowingTarget()
    {
        Spring.connectedBody = null;
        cuerda.SetActive(false);

    }

    public void Move()
    {

        var delta = Vector2.Distance(transform.position, Follows.position);

        if (delta <= deltaMaxDist && delta >= deltaMinToMove)
        {
            var direction = Vector2.MoveTowards(transform.position, Follows.position, Speed * Time.deltaTime);
            //transform.position = direction;
            _rigidbody2d.MovePosition(direction);
        }
    }

    public void Feed(Rigidbody2D distraction)
    {
        FollowTarget(distraction.transform);
    }




}
