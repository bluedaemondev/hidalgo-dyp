using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_M2Clickeable : MonoBehaviour
{
    public int healthHits;
    public float speedMov = 2;

    private Health health;

    Rigidbody2D _rigidbody;

    [SerializeField]
    float forceTakeoffSpeed;
    Vector2 forceTakeoff;
    [SerializeField]
    Animator animator;


    public GameObject particlesClick;

    public bool backing;
    public Vector3 originalPosition;

    private System.Action movement;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        movement = PursuitTarget;
        this.health = GetComponent<Health>();
        this._rigidbody = GetComponent<Rigidbody2D>();

        this.health.Init(healthHits, healthHits);
    }
    private void FixedUpdate()
    {
        movement();
    }

    private void OnMouseDown()
    {
        movement = delegate { };

        EffectFactory.instance.InstantiateEffectAt(particlesClick, transform.position, transform.rotation);
        var forceApply = CalculateTakeoffForce();

        GetComponent<Collider2D>().enabled = false;
        _rigidbody.AddForce(forceApply, ForceMode2D.Impulse);
        Destroy(this.gameObject, 3f);

    }
    private void PursuitTarget()
    {
        _rigidbody.MovePosition(Vector2.Lerp(transform.position, Vector3.zero, speedMov * Time.deltaTime));

        if (transform.position == Vector3.zero)
        {
            movement = GoOutOfScreen;

        }
    }
    private void GoOutOfScreen()
    {
        Debug.Log("getting out");
        _rigidbody.MovePosition(Vector2.Lerp(transform.position, originalPosition, speedMov * Time.deltaTime));

        if (transform.position == originalPosition)
        {
            Debug.Log("taken pickup");
            movement = delegate { };
            Destroy(this.gameObject, 1f);
        }
    }

    private Vector2 CalculateTakeoffForce()
    {
        Vector2 result = new Vector2();

        var direction = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = direction.normalized * forceTakeoffSpeed;

        return result;
    }
}
