using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocinante : MovementType
{
    [SerializeField] private float speed = 2;
    private float speedMultiplier = 1f;

    //public float deltaMidDist = 1;
    public float deltaMaxDist = 2;
    public float deltaMinToMove = 1.4f;
    private Transform _follows;
    //private RaycastHit2D raycast2D;

    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private LayerMask obstaclesLayermask;

    private Animator _animator;
    Vector3 lastPos;


    private Rigidbody2D _rigidbody2d;
    private Vector3 LastMoveDir;
    private float nextSoundTime = 0;
    [SerializeField]
    private AudioClip FootstepSound;
    [SerializeField]
    private AudioClip HorseSound;
    public float rocinanteState = 1;

    public event Action onSpringTargetChanged;

    //private SpringJoint2D spring;
    public GameObject cuerda;

    //private Action onUpdate;

    public InteractionWithPlayerQTE qteFollow;

    public Transform Follows { get => _follows; set { _follows = value; Debug.Log("following = " + _follows); onSpringTargetChanged(); } }
    public float Speed { get => speed * speedMultiplier; }

    Vector2 posAux;

    private void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        //Spring = GetComponent<SpringJoint2D>();
        lastPos = transform.position;
        if (qteFollow == null)
            qteFollow = transform.GetComponentInChildren<InteractionWithPlayerQTE>();

        //raycast2D = new RaycastHit2D();
    }

    private void OnDrawGizmos()
    {
        if (posAux != null)
        {
            Gizmos.DrawSphere(posAux, 0.4f);
        }
    }

    private void Update()
    {

        if (Follows != null && Follows.gameObject.layer == PickupsScapeGameManager.instance.Player.gameObject.layer) /*&& _rigidbody2d.velocity.magnitude > 0.2*/
        {

            //Debug.Log(_rigidbody2d.velocity.magnitude);

            var raycast2Dplayer = Physics2D.Raycast(Follows.position, transform.position, layerMask);

            /// actualizacion : implementado y deprecado 
            /// 1-7-21 a 1-7-21 
            /// RIP

            // implementar
            //var raycast2Dwalls = Physics2D.RaycastAll(Follows.position, transform.position, deltaMaxDist, obstaclesLayermask); 

            //foreach (var item in raycast2Dwalls)
            //{
            //    if (Vector2.Distance(item.point, raycast2Dplayer.point) < Vector2.Distance(transform.position, Follows.position))
            //    {
            //        Debug.Log("hay pared en el medio " + item.transform.name);
            //        return;
            //    }
            //}

            var DiferenciaX = raycast2Dplayer.point.x - transform.position.x;
            var DiferenciaY = raycast2Dplayer.point.y - transform.position.y;

            _animator.SetFloat("AnimMoveX", DiferenciaX);
            _animator.SetFloat("AnimMoveY", DiferenciaY);

            _animator.SetFloat("AnimLastMoveY", DiferenciaY);
            _animator.SetFloat("AnimLastMoveX", DiferenciaX);

            Move();

            // if (Vector2.Distance(transform.position, raycast2Dplayer.point) >= deltaMinToMove && Vector2.Distance(transform.position, raycast2Dplayer.point) <= deltaMaxDist)
            // {
            //     posAux = raycast2Dplayer.point;
            //     Debug.Log((posAux /** Speed * Time.deltaTime*/) + " " + this.Follows.gameObject.name);
            // }        

        }

        // if(!IsAttachedToPlayer())
        // {
        //     HudPlayerPickupScene.instance.NoCheckRocinante();
        // }

    }

    //private void FixedUpdate()
    //{
    //    //_rigidbody2d.MovePosition(Vector2.Lerp(transform.position, posAux, Speed * Time.deltaTime)); //(Vector2)posAux  /** Speed * Time.deltaTime*/
    //}

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
        //Spring.gameObject.SetActive(true);
        Follows = targetNew;

        //Spring.connectedBody = targetNew.GetComponent<Rigidbody2D>();
        //Spring.autoConfigureDistance = false;
        //Spring.distance = 4;

        cuerda.SetActive(true);
        // activar si la cuerda queda atada al nuevo punto
        //cuerda.GetComponent<FollowTargetOnUpdate>().targetFollow = targetNew;
        Debug.Log("jkaskdhjgajsdgh");
        onSpringTargetChanged();
        //cuerda.connectedAnchor = Vector2.zero;


    }

    public void StopFollowingTarget()
    {
        //Spring.connectedBody = null;
        cuerda.SetActive(false);

    }
    public void UncheckRocinante()
    {
        HudPlayerPickupScene.instance.NoCheckRocinante();
    }
    public void SetRocinanteState(float state)
    {
        if (_animator.GetFloat("RocinanteState") != state)
        {
            _animator.SetFloat("RocinanteState", state);
        }
    }

    public void Move()
    {

        var delta = Vector2.Distance(transform.position, Follows.position);

        if (delta <= deltaMaxDist && delta >= deltaMinToMove)
        {
            Vector2 direction = (Follows.position - transform.position).normalized * Speed * Time.deltaTime;

            if (direction.magnitude <= 0.2f)
                SetRocinanteState(1);
            //lel

            _rigidbody2d.MovePosition((Vector2)transform.position + direction);


            if (Time.time >= nextSoundTime)
            {
                SoundManager.instance.PlayEffect(FootstepSound);
                nextSoundTime = Time.time + FootstepSound.length;
            }

            SetRocinanteState(2);

            HudPlayerPickupScene.instance.CheckRocinante();
        }
        else if (delta >= deltaMaxDist)
        {
            SetRocinanteState(1);
            HudPlayerPickupScene.instance.NoCheckRocinante();
        }
        //Y... si no queda otra esto sirve

        else if (delta <= deltaMinToMove)
        {
            SetRocinanteState(1);
        }



        //RocinanteStateController();

    }

    //private void RocinanteStateController()
    //{
    //    var displacement = transform.position - lastPos;
    //    lastPos = transform.position;

    //    if (Follows.gameObject.layer == PickupsScapeGameManager.instance.Player.gameObject.layer && displacement.magnitude > 0.05)
    //    {
    //        if (Time.time >= nextSoundTime)
    //        {
    //            SoundManager.instance.PlayEffect(FootstepSound);
    //            nextSoundTime = Time.time + FootstepSound.length;
    //        }

    //        rocinanteState = 2;
    //        SetRocinanteState();
    //    }
    //    else if (Follows.gameObject.layer == PickupsScapeGameManager.instance.Player.gameObject.layer && displacement.magnitude < 0.05)
    //    {
    //        rocinanteState = 1;
    //        SetRocinanteState();
    //    }

    //}

    public void Feed(Rigidbody2D distraction)
    {

        SoundManager.instance.PlayEffect(HorseSound);
        FollowTarget(distraction.transform);

        var raycast2Ddistraction = Physics2D.Raycast(distraction.transform.position, transform.position);
        var DiferenciaEatingX = raycast2Ddistraction.point.x - transform.position.x;
        var DiferenciaEatingY = raycast2Ddistraction.point.y - transform.position.y;

        _animator.SetFloat("EatingMoveX", DiferenciaEatingX);
        _animator.SetFloat("EatingMoveY", DiferenciaEatingY);

        SetRocinanteState(3);

    }
    public bool IsAttachedToPlayer()
    {
        return this.Follows.gameObject.layer == PickupsScapeGameManager.instance.Player.gameObject.layer;
    }




}
