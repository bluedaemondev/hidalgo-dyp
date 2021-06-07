using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovementType, IStunneable, ISighteable
{
    public Rigidbody2D _rigidbody2D;
    public Animator myAnimator;
    public AudioClip currentFootstepSound;
    public AudioClip newFootstepSound;
    public AudioClip oldFootstepSound;
    [SerializeField] private float speed = 2.6f;
    private AudioSource audioSource;
    public float QuijoteState = 1;
    public float ArmorState = 1;

    [SerializeField]
    private AudioClip[] grassClips;
    [SerializeField]
    private AudioClip[] waterClips;

    public GameObject box;

    public void InitBoxControls()
    {
        this.GetStunned(0.25f);
        SwitchBoxActive();
        this._controller.canUseBox = true;
    }

    public void SwitchBoxActive()
    {
        var sw = !box.activeSelf;
        this.box.SetActive(sw);

        if (sw)
        {
            this.SetSpeedMultiplier(0);
            SoundManager.instance.PlayEffect(PickupsScapeGameManager.instance.soundLibrary.equipBox);
        }
        else
        {
            this.SetSpeedMultiplier(1);
            SoundManager.instance.PlayEffect(PickupsScapeGameManager.instance.soundLibrary.deEquipBox);

        }

    }

    [SerializeField] private float speedMultiplier = 1f;

    Movement _movement;
    Controller _controller;
    Sightable _sightable;

    public UnityEngine.UI.Slider sliderSeenStatus;
    public GameObject seenUiGO;

    public float Speed { get => speed * speedMultiplier; set => speed = value; }
    public float SpeedMultiplier { get => speedMultiplier; }
    public Movement Movement { get => _movement; }

    float originalMultiplier = 1;

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

        audioSource = GetComponent<AudioSource>();

        QuijoteState = 1f;
        ArmorState = 1f;
    }

    internal void DestunAfterTime(float v)
    {
        StopAllCoroutines();
        StartCoroutine(DestunAfter(v));
    }
    IEnumerator DestunAfter(float t)
    {
        yield return new WaitForSeconds(t);

        this.speedMultiplier = originalMultiplier;
    }
    public void SetSpeedMultiplier(float value)
    {
        this.speedMultiplier = value;
    }
    public void ResetSpeedMultipliers()
    {
        this.speedMultiplier = 1;
    }

    public void TeleportToPosition(Transform checkpoint)
    {
        StartCoroutine(SetDestination(checkpoint.position));
    }
    IEnumerator SetDestination(Vector3 position)
    {
        //this._movement.SwitchActive(false);
        //yield return null;

        Debug.Log("called");

        while (transform.position != position)
        {
            transform.position = Vector3.Lerp(transform.position, position, 3 * Time.deltaTime);
            yield return null;
        }

        //this._movement.SwitchActive(true);

    }
    private void Start()
    {
        _movement = new Movement(this);
        _controller = new Controller(Movement, this);
        _sightable = new Sightable(myAnimator, false, this, 5f, sliderSeenStatus);

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
    // public void PlayFootstepSound() //List<AudioClip> clips
    // {
    //     int rand = 0;
    //     rand = Random.Range(0,/* clips != null ? clips.Count :*/ PickupsScapeGameManager.instance.soundLibrary.quijoteStepGrass1.Count);
    //     SoundManager.instance.PlayEffect(/*clips != null ? clips[rand] :*/ PickupsScapeGameManager.instance.soundLibrary.quijoteStepGrass1[rand]);
    // }

    public void GetStunned(float timeStun)
    {
        StartCoroutine(CancelMovementInputFor(timeStun));
    }
    public void InvertControls(float timeStun)
    {
        StartCoroutine(InvertControllerFor(timeStun));
    }
    private IEnumerator InvertControllerFor(float time)
    {
        SoundManager.instance.PlayEffect(PickupsScapeGameManager.instance.soundLibrary.quijoteComplaining2);

        this.ChangeMyController();
        yield return new WaitForSeconds(time);
        this.RetrieveMyController();
    }

    public IEnumerator CancelMovementInputFor(float time)
    {
        SoundManager.instance.PlayEffect(PickupsScapeGameManager.instance.soundLibrary.quijoteComplaining1);

        originalMultiplier = this.speedMultiplier;

        this.speedMultiplier = 0;
        yield return new WaitForSeconds(time);
        this.speedMultiplier = originalMultiplier;

    }

    private void Step(AnimationEvent animationEvent)
    {
        if(animationEvent.animatorClipInfo.weight > 0.5)
        {
            AudioClip clip = GetRandomGrassClip();
            audioSource.PlayOneShot(clip);

            Debug.Log(clip);
        }
        
    }

    private AudioClip GetRandomGrassClip()
    {
        return grassClips[Random.Range(0, grassClips.Length)];
    }

    private AudioClip GetRandomWaterClip()
    {
        return waterClips[Random.Range(0, waterClips.Length)];
    }

    public void ChangeFootstepSound(AudioClip newFootstepSound)
    {
        oldFootstepSound = currentFootstepSound;

        currentFootstepSound = newFootstepSound;
    }

    public void RetrieveFootstepSound()
    {
        currentFootstepSound = oldFootstepSound;
    }

    public void Destun()
    {
        StopAllCoroutines();
        this.speedMultiplier = originalMultiplier;
    }
    public bool IsStunned()
    {
        return this.speedMultiplier == 0;
    }

    public void SetQuijoteState()
    {
        myAnimator.SetFloat("QuijoteState", QuijoteState);
    }

    public void SetArmorState()
    {
        myAnimator.SetFloat("ArmorState", ArmorState);
    }

    public void GetSeen(FieldOfView seenBy)
    {
        this.seenUiGO.SetActive(true);
        this._sightable.MarkAsSeen();
        myAnimator.Play("Seen");
        SoundManager.instance.PlayAmbient(PickupsScapeGameManager.instance.soundLibrary.playerSeen);

        StartCoroutine(CheckStillInView(seenBy));
    }

    public IEnumerator CheckStillInView(FieldOfView view)
    {
        while (this._sightable.IsSeen())
        {
            _sightable.UpdateFullySeenTime(Time.deltaTime);
            view.SetAimDirection(transform.position);
            //yield return new WaitForSeconds(0.5f);
            yield return null;

            if (!view.IsTransformInView(this.transform))
            {
                this._sightable.OutOfRange();
                seenUiGO.SetActive(false);
            }
        }
    }
    public IEnumerator UpdateSeenUI()
    {
        while (_sightable.UpdateFullySeenTime(Time.deltaTime))
        {
            yield return null;
        }

        _sightable.ResetFullySeenTime();

    }

    public Rigidbody2D GetRigidbody()
    {
        return this._rigidbody2D;
    }
}