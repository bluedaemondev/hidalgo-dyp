using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Rigidbody2D rigidbody2D;
    public float speed;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
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