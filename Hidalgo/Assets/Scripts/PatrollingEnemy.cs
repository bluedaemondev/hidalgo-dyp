using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : MonoBehaviour
{
    public Transform PointA;
    public Transform PointB;
    public float TravelTime = 1f;
    public Vector3 AuxScale;
    // public Animator myAnimator;

    public float SoldadoState = 2f;

    private float CurrentTravelTime;
    private bool MovingToB;

    private void Awake()
    {
       // myAnimator = GetComponent<Animator>();
    }

    private bool canMove = true;
    void Update()
    {
        if (canMove)
        {
            SoldadoState = 2f;
            SetSoldadoState();
            var DistanceAX = transform.position.x - PointA.position.x;
            var DistanceAY = transform.position.y - PointA.position.y;
            var DistanceBX = transform.position.x - PointB.position.x;
            var DistanceBY = transform.position.x - PointA.position.y;

            if (MovingToB)
            {
                CurrentTravelTime += Time.deltaTime;
                if (CurrentTravelTime >= TravelTime)
                {
                    MovingToB = false;
                   // FlipScale();
                    AwaitInPlace(2f);

                   //  myAnimator.SetFloat("SoldadoMoveX", DistanceBX);
                   //  myAnimator.SetFloat("SoldadoMoveY", DistanceBY);

                }
            }
            else
            {
                CurrentTravelTime -= Time.deltaTime;
                if (CurrentTravelTime <= 0f)
                {
                    MovingToB = true;
                   // FlipScale();
                    AwaitInPlace(2f);
                
                   //  myAnimator.SetFloat("SoldadoMoveX", DistanceAX);
                   //  myAnimator.SetFloat("SoldadoMoveY", DistanceAY);
                }
            }

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

    public void SetSoldadoState()
    {
       // myAnimator.SetFloat("SoldadoState", SoldadoState);
    }

    
}
