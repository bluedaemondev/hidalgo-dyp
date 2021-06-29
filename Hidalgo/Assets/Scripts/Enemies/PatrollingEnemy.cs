using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : MonoBehaviour
{
    [SerializeField]
    private float speed;
    public Transform PointA;
    public Transform PointB;
    public float TravelTime = 1f;
    public Vector3 AuxScale;
    public Animator myAnimator;

    public float SoldadoState = 2f;

    private float CurrentTravelTime;
    private bool MovingToB;

    private void Awake()
    {
        SoldadoState = 2f;
        myAnimator = GetComponent<Animator>();
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

                CurrentTravelTime += Time.deltaTime;
                if (CurrentTravelTime >= TravelTime)
                {
                    MovingToB = false;
                    //FlipScale();
                    AwaitInPlace(2f);

                    myAnimator.SetFloat("SoldadoMoveX", DistanceBX);
                    myAnimator.SetFloat("SoldadoMoveY", DistanceBY);
                }
            }
            else
            {

                CurrentTravelTime -= Time.deltaTime;
                if (CurrentTravelTime <= 0f)
                {
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

    public void SetSoldadoState()
    {
        myAnimator.SetFloat("SoldadoState", SoldadoState);
    }

    
}
