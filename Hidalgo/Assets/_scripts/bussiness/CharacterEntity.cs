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

    public CharacterEntity(float speed)
    {
        this._currentState = CharacterState.IDLE;
    }

    public virtual Vector2 CalculateInput()
    {
        /// llamar al script de gonzalo
        return Vector2.zero;
    }

    public void Move()
    {
        this._currentState = CharacterState.MOVING;
    }

    public CharacterState Update()
    {
        return this._currentState;
    }
}
