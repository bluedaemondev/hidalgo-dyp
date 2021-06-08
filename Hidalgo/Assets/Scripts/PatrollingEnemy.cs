using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : MonoBehaviour
{
    public Transform PointA;
    public Transform PointB;
    public float TravelTime = 1f;
    public Vector3 AuxScale;
    public Animator myAnimator;

    private float SoldadoState = 2;

    private float CurrentTravelTime;
    private bool MovingToB;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    private bool canMove = true;
    void Update()
    {
        if (canMove)
        {
            if (MovingToB)
            {
                CurrentTravelTime += Time.deltaTime;
                if (CurrentTravelTime >= TravelTime)
                {
                    MovingToB = false;
                    FlipScale();
                    AwaitInPlace(2f);

                  
                }
            }
            else
            {
                CurrentTravelTime -= Time.deltaTime;
                if (CurrentTravelTime <= 0f)
                {
                    MovingToB = true;
                    FlipScale();
                    AwaitInPlace(2f);

                    
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
        SoldadoState = 1;
        yield return new WaitForSeconds(time);
        canMove = true;
        SoldadoState = 2;

    }
    private void FlipScale()
    {
        AuxScale = transform.localScale;
        AuxScale.x = -AuxScale.x;
        transform.localScale = AuxScale;
        
    }

    
}
