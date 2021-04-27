
using UnityEngine;

public class Movement
{
    Player _player;
    private Vector3 moveDir;

    public Movement(Player p)
    {
        _player = p;
    }

    public void Move(float moveX, float moveY)
    {
        moveDir = new Vector3(moveX, moveY).normalized;
        

        _player.GetComponent<Rigidbody2D>().velocity = moveDir * _player.Speed;

        if (_player.GetComponent<Rigidbody2D>().velocity.magnitude > 0.1f)
            _player.GetComponent<AnimatedCharacterController>().State = CharacterState.MOVING;            
        else
            _player.GetComponent<AnimatedCharacterController>().State = CharacterState.IDLE;

    }
}
