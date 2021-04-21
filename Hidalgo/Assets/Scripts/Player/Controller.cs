using UnityEngine;

public class Controller
{
    Movement _movement;

    public Controller(Movement m)
    {
        _movement = m;
    }

    public void OnUpdate()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.D))
        {
            moveX = +1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }

        if (Input.GetKey(KeyCode.W))
        {
            moveY = +1f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveY = -1f;
        }

        
        
         _movement.Move(moveX, moveY);
        

    }
}