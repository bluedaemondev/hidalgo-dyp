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

    public SpringJoint2D cuerda;

    
    public Transform Follows { get => _follows; set { _follows = value; Debug.Log("following = " + _follows); } }
    public float Speed { get => speed * speedMultiplier; }

    private void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void SetSpeedMultiplier(float value)
    {
        this.speedMultiplier = value;
    }
    public void ResetSpeedMultipliers()
    {
        this.speedMultiplier = 1;
    }

    public void FollowTarget()
    {
        cuerda.gameObject.SetActive(true);
        cuerda.connectedBody = Follows.GetComponent<Rigidbody2D>();
        cuerda.connectedAnchor = Follows.transform.position;
    }
    public void StopFollowingTarget()
    {
        cuerda.connectedBody = null;
        cuerda.gameObject.SetActive(false);
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




}
