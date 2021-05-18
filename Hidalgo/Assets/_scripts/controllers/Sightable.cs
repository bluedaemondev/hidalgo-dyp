using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sightable
{
    private bool _inSight;
    private Animator _animator;
    MovementType _parent;

    private float fullySeenTime;
    private float currentSeenTime;

    UnityEngine.UI.Slider sliderSeenValue;

    public Sightable(Animator animator, bool inSight, MovementType parent, float timeToFullySeen, Slider seenMeter)
    {
        this._animator = animator;
        this._inSight = inSight;
        this._parent = parent;
        this.fullySeenTime = timeToFullySeen;
        this.currentSeenTime = 0;

        this.sliderSeenValue = seenMeter;
        this.sliderSeenValue.maxValue = fullySeenTime;
        this.sliderSeenValue.minValue = 0;

    }
    public bool IsSeen()
    {
        return this._inSight;
    }
    public void MarkAsSeen()
    {
        this._inSight = true;
    }
    public void OutOfRange()
    {
        ResetFullySeenTime();
        this._inSight = false;
    }

    public bool UpdateFullySeenTime(float tAdd)
    {
        this.currentSeenTime += tAdd;

        this.sliderSeenValue.value = Mathf.Clamp(this.fullySeenTime + tAdd, 0, this.sliderSeenValue.maxValue);
        if (this.sliderSeenValue.value >= this.sliderSeenValue.maxValue)
        {
            currentSeenTime = 0;
            this._inSight = false;
            return true;
        }
        else
            return false;

    }
    public void ResetFullySeenTime()
    {
        this.sliderSeenValue.value = 0;
    }
}
