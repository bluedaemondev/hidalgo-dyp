using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTopDown : MonoBehaviour
{
    public static readonly int hashIdle = Animator.StringToHash("Idle");
    public static readonly int hashMoving = Animator.StringToHash("Moving");
    public static readonly int hashPreStuned = Animator.StringToHash("PreStuned");
    public static readonly int hashStuned = Animator.StringToHash("Stuned");

    [SerializeField]
    CharacterEntity _entity;
    [SerializeField]
    Animator _animator;

    CharacterState state;

    // Start is called before the first frame update
    void Start()
    {
        if (_entity == null)
        {
            _entity = new CharacterEntity(8);
        }

        _animator = this.GetComponent<Animator>();
    }

    /// <summary>
    /// Esta clase solo maneja estados de la entidad. la logica esta metida en _entity
    /// Los comportamientos estan en el MSB de cada estado del animator
    /// </summary>
    void Update()
    {
        switch (_entity.Update())
        {
            case CharacterState.IDLE:
                _animator.Play(hashIdle);
                break;
            case CharacterState.MOVING:
                _animator.Play(hashMoving);
                break;
            case CharacterState.PRE_STUN:
                _animator.Play(hashPreStuned);
                break;
            case CharacterState.STUNNED:
                _animator.Play(hashStuned);
                break;
        }
    }
}
