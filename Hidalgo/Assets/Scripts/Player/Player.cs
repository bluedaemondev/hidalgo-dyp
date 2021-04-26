using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovementType
{
    public Rigidbody2D _rigidbody2D;
    [SerializeField] private float speed = 2.6f;
    [SerializeField] private float speedMultiplier = 1f;
    public float SpeedMultiplier { get => speedMultiplier; }

    public float Speed { get => speed * speedMultiplier; private set => speed = value; }

    public void SetSpeedMultiplier(float value)
    {
        this.speedMultiplier = value;
    }
    public void ResetSpeedMultipliers()
    {
        this.speedMultiplier = 1;
    }
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    Controller _controller;
    Movement _movement;


    private void Start()
    {
        _movement = new Movement(this);
        _controller = new Controller(_movement);
    }

    private void Update()
    {
        _controller.OnUpdate();
    }
}