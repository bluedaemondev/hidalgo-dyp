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
    private RaycastHit2D raycast2D;
    private LayerMask layerMask;
    private Animator _animator;
    private Rigidbody2D _rigidbody2d;

    public SpringJoint2D cuerda;




    public Transform Follows { get => _follows; set { _follows = value; Debug.Log("following = " + _follows); } }
    public float Speed { get => speed * speedMultiplier; }

    private void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        raycast2D = Physics2D.Raycast(Follows.position, transform.position, layerMask);

        var DiferenciaX = raycast2D.point.x - transform.position.x;

        var DiferenciaY = raycast2D.point.y - transform.position.y;

        _animator.SetFloat("AnimMoveX", DiferenciaX);
        _animator.SetFloat("AnimMoveY", DiferenciaY);

        Debug.Log("X " + DiferenciaX);
        Debug.Log("Y " + DiferenciaY);
        Debug.Log("Quijote está en " + Follows.position);
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
        cuerda.gameObject.SetActive(true);
        Follows = targetNew;
        cuerda.connectedBody = targetNew.GetComponent<Rigidbody2D>();
        cuerda.autoConfigureDistance = false;

        cuerda.distance = 3.5f;
        //cuerda.connectedAnchor = Vector2.zero;


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

       /* raycast2D = Physics2D.Raycast(Follows.position, transform.position, layerMask);

        var DiferenciaX = raycast2D.point.x - transform.position.x;

        var DiferenciaY = raycast2D.point.y - transform.position.y;

        _animator.SetFloat("AnimMoveX", DiferenciaX);
        _animator.SetFloat("AnimMoveY", DiferenciaY);
        */
    }

    public void Feed(Rigidbody2D distraction)
    {
        FollowTarget(distraction.transform);
    }




}
