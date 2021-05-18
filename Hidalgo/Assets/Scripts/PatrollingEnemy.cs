using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : MonoBehaviour
{
    public Transform PointA;
    public Transform PointB;
    public float TravelTime = 1f;
    public Vector3 AuxScale;

    private float CurrentTravelTime;
    private bool MovingToB;
    void Update()
    {
        if (MovingToB)
        {
            CurrentTravelTime += Time.deltaTime;
            if (CurrentTravelTime >= TravelTime)
            {
                MovingToB = false;
                FlipScale();
            }
        }
        else
        {
            CurrentTravelTime -= Time.deltaTime;
            if (CurrentTravelTime <= 0f)
            {
                MovingToB = true;
                FlipScale();


            }
        }

        transform.position = Vector3.Lerp(PointA.position, PointB.position, CurrentTravelTime / TravelTime);
    }

    private void FlipScale()
    {
        AuxScale = transform.localScale;
        AuxScale.x = -AuxScale.x;
        transform.localScale = AuxScale;
    }
}
