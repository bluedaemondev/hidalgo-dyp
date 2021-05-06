using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimateStateModifier : MonoBehaviour, IScalable
{
    private Vector2 localScaleOriginal;
    [SerializeField] private Vector2 localScaleMax;
    [SerializeField] private Vector2 localScaleMin;

    [Range(0f, 1f)] public float smoothFactor = 0.4f;

    private void Start()
    {
        this.localScaleOriginal = transform.localScale;
        
    }

    public void ActionBasedOnClimate(ClimateState currentStateClimate)
    {
        switch (currentStateClimate)
        {
            case ClimateState.CLEAR:
                StopAllCoroutines();
                ResetScale();
                break;

            case ClimateState.RAIN:
                StartCoroutine(MixBetweenMaxMin());
                break;

            case ClimateState.STORM:
                StopAllCoroutines();
                MaximizeScale();
                break;
        }

    }

    private IEnumerator MixBetweenMaxMin(float timePerState = 2f)
    {
        for (int i = 0; i < 4; i++) // test
        {
            MinimizeScale();
            yield return new WaitForSeconds(timePerState);
            MaximizeScale();
            yield return null;
        }

    }

    private IEnumerator ScaleInterpolate(Vector2 target)
    {
        while ((Vector2)this.transform.localScale != target)
        {
            this.transform.localScale = Vector2.Lerp(transform.localScale, target, this.smoothFactor * Time.deltaTime);
            yield return null;
        }
        Debug.Log("target reached");
    }
    public void MaximizeScale()
    {
        StartCoroutine(ScaleInterpolate(localScaleMax));
    }

    public void MinimizeScale()
    {
        StartCoroutine(ScaleInterpolate(localScaleMin));
    }

    public void ResetScale()
    {
        StartCoroutine(ScaleInterpolate(localScaleOriginal));
    }
}
