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
    //private RaycastHit2D raycast2D;
    [SerializeField]
    private LayerMask layerMask;
    private Animator _animator;

    

    private Rigidbody2D _rigidbody2d;
    private float nextSoundTime = 0;
    [SerializeField]
    private AudioClip FootstepSound;
    [SerializeField]
    private AudioClip HorseSound;

    public event Action onSpringTargetChanged;

    private SpringJoint2D spring;
    public GameObject cuerda;


    public InteractionWithPlayerQTE qteFollow;

    public Transform Follows { get => _follows; set { _follows = value; Debug.Log("following = " + _follows); } }
    public float Speed { get => speed * speedMultiplier; }
    public SpringJoint2D Spring { get => spring; set { spring = value; onSpringTargetChanged?.Invoke(); } }

    private void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        Spring = GetComponent<SpringJoint2D>();

        if (qteFollow == null)
            qteFollow = transform.GetComponentInChildren<InteractionWithPlayerQTE>();

        //raycast2D = new RaycastHit2D();
    }

    private void Update()
    {
        if (Follows != null)
        {
            _animator.SetBool("IsMoving", true);

            if (Time.time >= nextSoundTime)
            {
                SoundManager.instance.PlayEffect(FootstepSound);
                nextSoundTime = Time.time + FootstepSound.length;
            }

            var raycast2D = Physics2D.Raycast(Follows.position, transform.position, layerMask);

            var DiferenciaX = raycast2D.point.x - transform.position.x;

            var DiferenciaY = raycast2D.point.y - transform.position.y;

            _animator.SetFloat("AnimMoveX", DiferenciaX);
            _animator.SetFloat("AnimMoveY", DiferenciaY);

        }

        //Debug.Log("X " + DiferenciaX);
        //Debug.Log("Y " + DiferenciaY);
        //Debug.Log("Quijote está en " + Follows.position);

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
        Spring.distance = 4;

        cuerda.SetActive(true);
        // activar si la cuerda queda atada al nuevo punto
        //cuerda.GetComponent<FollowTargetOnUpdate>().targetFollow = targetNew;
        
        onSpringTargetChanged?.Invoke();
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

        SoundManager.instance.PlayEffect(HorseSound);
        FollowTarget(distraction.transform);

    }
    public bool IsAttachedToPlayer()
    {
        return this.spring.connectedBody != null && this.spring.connectedBody == PickupsScapeGameManager.instance.Player.GetRigidbody();
    }




}
