using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Controlador para el jugador, los compañeros y cualquier entidad que comparta los 4 estados del animator
/// Crea un objeto de entidad al iniciar la partida
/// </summary>
public class AnimatedCharacterController : MonoBehaviour
{
    public static readonly int hashIdle = Animator.StringToHash("Idle");
    public static readonly int hashMoving = Animator.StringToHash("Moving");
    public static readonly int hashPreStuned = Animator.StringToHash("PreStuned");
    public static readonly int hashStuned = Animator.StringToHash("Stuned");

    [SerializeField]
    CharacterEntity _entity;
    [SerializeField]
    Animator _animator;

    //public UnityEvent onMove;
    //public UnityEvent onIdle;
    //public UnityEvent onPreStun;
    //public UnityEvent onStunned;

    /// <summary>
    /// PROTOTIPO, ver (!)
    /// </summary>
    public PrototypeMoveTowards mover;

    CharacterState _state;
    public CharacterState State
    {
        get => _state;
        set
        {
            if (_entity != null)
                _entity._currentState = value;
            this._state = value;
        }
    }


    private void Awake()
    {
        //onIdle = new UnityEvent();
        //onMove = new UnityEvent();
        //onPreStun = new UnityEvent();
        //onStunned = new UnityEvent();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_entity == null)
        {
            _entity = new CharacterEntity(8);
        }
        if(gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            mover = GetComponent<PrototypeMoveTowards>();
        }

        _animator = this.GetComponent<Animator>();
    }

    /// <summary>
    /// Esta clase solo maneja estados de la entidad. la logica esta metida en _entity
    /// Los comportamientos estan en el MSB de cada estado del animator
    /// invoca extras que necesitemos por eventos
    /// </summary>
    void Update()
    {
        switch (_entity.Update())
        {
            case CharacterState.IDLE:
                _animator.Play(hashIdle);
                _animator.SetBool("isMoving", false);

                //onIdle.Invoke();
                break;
            case CharacterState.MOVING:
                _animator.Play(hashMoving);
                _animator.SetBool("isMoving", true);

                mover.Move();
                //onMove.Invoke();
                break;
            case CharacterState.PRE_STUN:
                _animator.Play(hashPreStuned);
                //onPreStun.Invoke();
                break;
            case CharacterState.STUNNED:
                _animator.Play(hashStuned);
                //onStuned.Invoke();
                break;
        }
    }
}
