using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovementType, IStunneable
{
    public Rigidbody2D _rigidbody2D;
    public Animator myAnimator;
    public AudioClip footsteps;
    [SerializeField] private float speed = 2.6f;
    [SerializeField] private float speedMultiplier = 1f;

    Movement _movement;
    Controller _controller;

    public float Speed { get => speed * speedMultiplier; set => speed = value; }
    public float SpeedMultiplier { get => speedMultiplier; }
    public Movement Movement { get => _movement; }


    // public float speed;
    public List<KeyCode> OMovement;
    public List<KeyCode> NMovement;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        OMovement = new List<KeyCode>();
        NMovement = new List<KeyCode>();

        OMovement.Add(KeyCode.W);
        OMovement.Add(KeyCode.S);
        OMovement.Add(KeyCode.A);
        OMovement.Add(KeyCode.D);

        NMovement.Add(KeyCode.S);
        NMovement.Add(KeyCode.W);
        NMovement.Add(KeyCode.D);
        NMovement.Add(KeyCode.A);
    }
    public void SetSpeedMultiplier(float value)
    {
        this.speedMultiplier = value;
    }
    public void ResetSpeedMultipliers()
    {
        this.speedMultiplier = 1;
    }


    private void Start()
    {
        _movement = new Movement(this);
        _controller = new Controller(Movement);

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

    /// para animation event
    /// 
    public void PlayFootstepSound() //List<AudioClip> clips
    {
        int rand = 0;
        rand = Random.Range(0,/* clips != null ? clips.Count :*/ PickupsScapeGameManager.instance.soundLibrary.quijoteStepGrass1.Count);
        SoundManager.instance.PlayEffect(/*clips != null ? clips[rand] :*/ PickupsScapeGameManager.instance.soundLibrary.quijoteStepGrass1[rand]);
    }

    public void GetStunned(float timeStun)
    {
        StartCoroutine(CancelMovementInputFor(timeStun));
    }

    public IEnumerator CancelMovementInputFor(float time)
    {
        var originalMultiplier = this.speedMultiplier;

        this.speedMultiplier = 0;
        yield return new WaitForSeconds(time);
        this.speedMultiplier = originalMultiplier;
        
    }


    public void Destun()
    {
        Debug.Log("? deprecate");
    }
    public bool IsStunned()
    {
        return this.speedMultiplier == 0;
    }
    
}