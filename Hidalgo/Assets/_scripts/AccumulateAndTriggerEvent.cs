using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AccumulateAndTriggerEvent : MonoBehaviour
{
    public float currentTime;
    public float tFactor;
    public float maxTime;

    public UnityEvent triggerOnFull;

    private void OnTriggerStay2D(Collider2D collision)
    {
        this.currentTime += Time.deltaTime;
        if (tFactor < 1)
        {
            tFactor += Time.deltaTime / maxTime;
            //maskInteraction.transform.localScale = Vector3.Lerp(originalScaleMask, Vector3.zero, tFactor);
        }

        if ((currentTime >= maxTime || tFactor >= 1))
        {
            triggerOnFull.Invoke();
            this.currentTime = 0;
            this.tFactor = 0;
        }
    }
}
