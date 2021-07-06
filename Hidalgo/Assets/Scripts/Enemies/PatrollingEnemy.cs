using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : MonoBehaviour
{
    [SerializeField]
    Player player;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float speed;
    public Transform PointA;
    public Transform PointB;
    public float TravelTime = 1f;
    public Vector3 AuxScale;
    private float originalTravelTime;
    Vector3 originalPointB;
    Vector3 originalPointA;

    public Animator myAnimator;

    [SerializeField]
    private float DiferenciaYB1;

    [SerializeField]
    private float DiferenciaYB2;

    [SerializeField]
    private float DiferenciaYA1;

    [SerializeField]
    private float DiferenciaYA2;

    [SerializeField]
    private float DiferenciaXB1;

    [SerializeField]
    private float DiferenciaXB2;

    [SerializeField]
    private float DiferenciaXA1;

    [SerializeField]
    private float DiferenciaXA2;

    public float SoldadoState = 2f;

    private float CurrentTravelTime;
    private bool MovingToB;

    private void Awake()
    {
        SoldadoState = 2f;
        myAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        originalPointA = PointA.position;
        originalPointB = PointB.position;
        originalTravelTime = TravelTime;
    }

    private bool canMove = true;
    void Update()
    {
        if (canMove)
        {
            SoldadoState = 2f;
            SetSoldadoState();
            var DistanceAX = PointA.position.x - transform.position.x;
            var DistanceAY = PointA.position.y - transform.position.y;
            var DistanceBX = PointB.position.x - transform.position.x;
            var DistanceBY = PointB.position.y - transform.position.y;

            if (MovingToB)
            {

                var raycast2DB = Physics2D.Raycast(player.transform.position, transform.position, layerMask);
                var DiferenciaY = raycast2DB.point.y - transform.position.y;
                var DiferenciaX = raycast2DB.point.x - transform.position.x;

                Debug.Log("B" + DiferenciaY);
                Debug.Log("BX" + DiferenciaX);

                if (DiferenciaY < DiferenciaYB1 && DiferenciaY > DiferenciaYB2 && DiferenciaX >= DiferenciaXB1 && DiferenciaX <= DiferenciaXB2 && player.OnBox)
                {
                    PointB.position = transform.position;
                    CurrentTravelTime = TravelTime;
                    TravelTime = TravelTime / 2;

                }

                CurrentTravelTime += Time.deltaTime;
                if (CurrentTravelTime >= TravelTime)
                {
                    ResetPointA();
                    ResetTravelTime();

                    MovingToB = false;
                    //FlipScale();
                    AwaitInPlace(2f);


                    myAnimator.SetFloat("SoldadoMoveX", DistanceBX);
                    myAnimator.SetFloat("SoldadoMoveY", DistanceBY);
                }
            }
            else
            {
                var raycast2DA = Physics2D.Raycast(player.transform.position, transform.position, layerMask);
                var DiferenciaY = raycast2DA.point.y - transform.position.y;
                var DiferenciaX = raycast2DA.point.x - transform.position.x;

                Debug.Log("A" + DiferenciaY);
                Debug.Log("AX" + DiferenciaX);

                if (DiferenciaY > DiferenciaYA1 && DiferenciaY < DiferenciaYA2 && DiferenciaX >= DiferenciaXA1 && DiferenciaX <= DiferenciaXA2 && player.OnBox)
                {
                    PointA.position = transform.position;
                    CurrentTravelTime = 0;
                    TravelTime = TravelTime / 2;
                    
                }

                CurrentTravelTime -= Time.deltaTime;
                if (CurrentTravelTime <= 0f)
                {
                    ResetPointB();
                    ResetTravelTime();

                    MovingToB = true;
                    //FlipScale();
                    AwaitInPlace(2f);


                    myAnimator.SetFloat("SoldadoMoveX", DistanceAX);
                    myAnimator.SetFloat("SoldadoMoveY", DistanceAY);                
                }
            }

            //transform.position = Vector3.MoveTowards(PointA.position, PointB.position, speed * Time.deltaTime);
             transform.position = Vector3.Lerp(PointA.position, PointB.position, CurrentTravelTime / TravelTime);
        }
    }
    private void AwaitInPlace(float time)
    {
        StartCoroutine(LockMovementFor(time));
    }
    private IEnumerator LockMovementFor(float time)
    {
        canMove = false;
        SoldadoState = 1f;
        SetSoldadoState();
        yield return new WaitForSeconds(time);
        canMove = true;
        SoldadoState = 2f;
        SetSoldadoState();

    }
    private void FlipScale()
    {
        AuxScale = transform.localScale;
        AuxScale.x = -AuxScale.x;
        transform.localScale = AuxScale;
        
    }

    public void SetTravelTime (float newTime)
    {
        TravelTime = newTime;
    }

    private void ResetPointB()
    {
        PointB.position = originalPointB;
    }

    private void ResetTravelTime()
    {
        TravelTime = originalTravelTime;
    }

    private void ResetPointA()
    {
        PointA.position = originalPointA;
    }

    public void SetSoldadoState()
    {
        myAnimator.SetFloat("SoldadoState", SoldadoState);
    }

    
}
