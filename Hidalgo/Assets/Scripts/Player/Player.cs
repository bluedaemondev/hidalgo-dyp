using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Rigidbody2D rigidbody2D;
    public float speed;
    public List<KeyCode> OMovement = new List<KeyCode>();
    public List<KeyCode> NMovement = new List<KeyCode>();
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        OMovement[0] = KeyCode.W;
        OMovement[1] = KeyCode.S;
        OMovement[2] = KeyCode.A;
        OMovement[3] = KeyCode.D;

        NMovement[0] = KeyCode.S;
        NMovement[1] = KeyCode.W;
        NMovement[2] = KeyCode.D;
        NMovement[3] = KeyCode.A;

    }

    Controller _controller;
    Movement _movement;

    private void Start()
    {
        _movement = new Movement(this);
        _controller = new Controller(_movement);

        _controller.OnStart();
    }
    
    private void Update()
    {
        _controller.OnUpdate();
    }

    public void ChangeMyController()
    {
        _controller.ChangeMyMovement();
    }

    public void RetrieveMyController()
    {
        _controller.RetrieveMyMovement();
    }

}