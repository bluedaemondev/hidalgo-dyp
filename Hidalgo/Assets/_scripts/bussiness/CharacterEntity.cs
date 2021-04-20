using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState
{
    IDLE,
    MOVING,
    PRE_STUN,
    STUNNED
}

[System.Serializable]
public class CharacterEntity : IMovable
{
    public CharacterState _currentState;
    public float _speedMov;

    public CharacterEntity(float speed)
    {
        this._currentState = CharacterState.IDLE;
        this._speedMov = speed;
    }

    public virtual Vector2 CalculateInput()
    {
        /// llamar al script de gonzalo
        return Vector2.zero;
    }

    public Vector2 Move(Vector2 direction)
    {
        this._currentState = CharacterState.MOVING;
        Debug.Log("moving");

        return (CalculateInput() + direction) * _speedMov * Time.deltaTime;
    }
}
