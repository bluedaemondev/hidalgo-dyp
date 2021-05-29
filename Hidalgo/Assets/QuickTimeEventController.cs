using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuickTimeEventController : MonoBehaviour
{
    [SerializeField]
    private Transform handle;
    [SerializeField]
    private Transform startingPoint;
    [SerializeField]
    private Transform endingPoint;

    [Header("Tiempo total en seg. que tarda en pasar de izq a der el handle")]
    public float timeTotalInQte = 6f;

    private bool passed;

    public int requiredTotal = 2;
    private int currentTotal;

    public InteractionWithPlayerQTE interactionBase;

    public bool Passed
    {
        get => passed;
        set
        {
            if (value)
            {
                Debug.Log("hjkasgdjhgb no tenajiksghdka");
                SoundManager.instance.PlayEffect(PickupsScapeGameManager.instance.soundLibrary.completedQteGeneric);
                if (interactionBase != null)
                    interactionBase.onPassed?.Invoke();

            }
            else
            {
                SoundManager.instance.PlayEffect(PickupsScapeGameManager.instance.soundLibrary.wrongKey);
                if (interactionBase != null)
                    interactionBase.onFailed?.Invoke();
            }


        }
    }
    public int RequiredTotal
    {
        get => requiredTotal;
    }

    public int CurrentTotal
    {
        get => currentTotal;
        set
        {
            currentTotal = value;
            Debug.Log(currentTotal);
            SoundManager.instance.PlayEffect(PickupsScapeGameManager.instance.soundLibrary.correctKey);

            if (currentTotal >= requiredTotal)
            {
                SoundManager.instance.PlayEffect(PickupsScapeGameManager.instance.soundLibrary.completedQteGeneric);
                Passed = true;

            }
        }
    }

    /// <summary>
    /// Muevo desde starting a ending point
    /// tiene una funcion en base al tiempo indicado en el componente
    /// </summary>
    /// <returns></returns>
    private IEnumerator SlideHandle()
    {
        var currentPos = startingPoint.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeTotalInQte;
            handle.position = Vector3.Lerp(currentPos, endingPoint.position, t);
            yield return null;
        }

        PickupsScapeGameManager.instance.Player.Destun();
        Destroy(this.gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        this.passed = false;

        PickupsScapeGameManager.instance.Player.GetStunned(timeTotalInQte);
        StartCoroutine(SlideHandle());
    }

}
