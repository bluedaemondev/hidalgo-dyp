using UnityEngine;

public class Controller
{
    Movement _movement;
    Player _player;
    //public Controller (Player p)
    //{
    //    _player = p;
    //}

    public KeyCode MoveUp;
    public KeyCode MoveDown;
    public KeyCode MoveLeft;
    public KeyCode MoveRight;

    public KeyCode UseBox;

    public bool canUseBox;

    public Controller(Movement m, Player p)
    {
        _movement = m;
        _player = p;
    }

    public void OnStart()
    {
        MoveUp = KeyCode.W;
        MoveDown = KeyCode.S;
        MoveLeft = KeyCode.A;
        MoveRight = KeyCode.D;
        UseBox = KeyCode.Space;
    }

    public void OnUpdate()
    {
        float moveX = 0f;
        float moveY = 0f;

        bool boxUsed = Input.GetKeyDown(UseBox);

        //Debug.Log("box used");

        if (Input.GetKey(MoveRight))
        {
            moveX = +1f;
        }

        if (Input.GetKey(MoveLeft))
        {
            moveX = -1f;
        }

        if (Input.GetKey(MoveUp))
        {
            moveY = +1f;
        }

        if (Input.GetKey(MoveDown))
        {
            moveY = -1f;
        }

        
        if(boxUsed && canUseBox)
            _player.SwitchBoxActive();

        _movement.Move(moveX, moveY);

    }
    
    public void ChangeMyMovement()
    {
        MoveUp = KeyCode.S;
        MoveDown = KeyCode.W;
        MoveLeft = KeyCode.D;
        MoveRight = KeyCode.A;
    }

    public void RetrieveMyMovement()
    {
        MoveUp = KeyCode.W;
        MoveDown = KeyCode.S;
        MoveLeft = KeyCode.A;
        MoveRight = KeyCode.D;
    }

    
}