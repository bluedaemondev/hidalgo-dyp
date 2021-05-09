using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimateStateModifier : MonoBehaviour, IScalable
{
    private Vector2 localScaleOriginal;
    [SerializeField] private Vector2 localScaleMax;
    [SerializeField] private Vector2 localScaleMin;

    [Range(0f, 3f)] public float smoothFactor = 0.4f;

    public float timeBetweenStates = 3f;

    private void Start()
    {
        this.localScaleOriginal = transform.localScale;
        ClimateController.instance.onChangeClimate.AddListener(this.ActionBasedOnClimate);
    }

    public void ActionBasedOnClimate(ClimateState currentStateClimate)
    {
        switch (currentStateClimate)
        {
            // centinelas que estan en las ventanas o se mueven patrullando entre puntos
            case ClimateState.CLEAR:
                StopAllCoroutines();
                ResetScale();
                break;
            
            // crecen los charcos y se achican despues de un tiempo
            case ClimateState.RAIN:
                StartCoroutine(MixBetweenMaxMin());
                break;
            
            // D
            case ClimateState.STORM:
                StopAllCoroutines();
                MaximizeScale();
                break;
        }

    }

    private IEnumerator MixBetweenMaxMin()
    {
        while (true) // test
        {
            MinimizeScale();
            yield return new WaitForSeconds(timeBetweenStates);
            ResetScale();
            yield return new WaitForSeconds(timeBetweenStates);
            MaximizeScale();
            yield return new WaitForSeconds(timeBetweenStates);
        }

    }

    private IEnumerator ScaleInterpolate(Vector2 target)
    {
        while ((Vector2)this.transform.localScale != target)
        {
            this.transform.localScale = Vector2.Lerp(transform.localScale, target, this.smoothFactor * Time.deltaTime);
            yield return null;
        }
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
