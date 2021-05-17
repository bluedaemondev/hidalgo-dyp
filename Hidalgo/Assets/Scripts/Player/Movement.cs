
using UnityEngine;

public class Movement
{
    Player _player;
    private Vector3 moveDir;
    private Vector3 lastMoveDir;
    float nextSoundTime = 0;
    public Movement(Player p)
    {
        _player = p;
    }

    public void Move(float moveX, float moveY)
    {
        moveDir = new Vector3(moveX, moveY).normalized;

        _player.myAnimator.SetFloat("AnimMoveX", moveDir.x);
        _player.myAnimator.SetFloat("AnimMoveY", moveDir.y);
        _player.myAnimator.SetFloat("AnimMoveMagnitude", moveDir.magnitude);
       

        _player._rigidbody2D.velocity = moveDir * _player.Speed;

        if (_player._rigidbody2D.velocity.magnitude > 0.1f)
        {
            _player.GetComponent<AnimatedCharacterController>().State = CharacterState.MOVING;

            if(Time.time>=nextSoundTime)
            {
                SoundManager.instance.PlayEffect(_player.footsteps);
                nextSoundTime = Time.time + _player.footsteps.length;
            }
        }
        else
        {
            _player.GetComponent<AnimatedCharacterController>().State = CharacterState.IDLE;
        }

        if ((moveX == 0 || moveY == 0) && moveDir.x != 0 || moveDir.y != 0)
        {
            lastMoveDir = moveDir;
        }

        _player.myAnimator.SetFloat("AnimLastMoveX", lastMoveDir.x);
        _player.myAnimator.SetFloat("AnimLastMoveY", lastMoveDir.y);

        Debug.Log(lastMoveDir);
     
    }
}
