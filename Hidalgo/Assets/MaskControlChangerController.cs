using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Implementar con animator a futuro para dar una mejor transicion
/// </summary>
public class MaskControlChangerController : MonoBehaviour
{
    public SpriteRenderer sRenderMaskBase;
    [Range(0.03f, 1f)] public float factorFade = 1f;

    [SerializeField] private bool isProcessing = false;

    public List<GameObject> children;

    // Start is called before the first frame update
    void Start()
    {
        if (children == null || children.Count == 0)
        {
            children = new List<GameObject>();
            children = transform.GetComponentsInChildren<GameObject>().ToList();
        }
    }

    public void FadeInMask(float changeControlsTime)
    {
        if (!isProcessing)
            StartCoroutine(FadeMask(changeControlsTime, true));

        Debug.Log("on fade in mask");
    }
    public void FadeOutMask(float fadeoutTime)
    {
        StartCoroutine(FadeMask(fadeoutTime, false));
        Debug.Log("on fade out mask");
    }

    private IEnumerator FadeMask(float tTotal, bool fadeIn)
    {
        isProcessing = true;
        foreach (var chld in children)
            chld.SetActive(true);

        yield return null;


        Color colorTmp = sRenderMaskBase.color;
        float timer = 0;

        while ((fadeIn && sRenderMaskBase.color.a < 1 || !fadeIn && sRenderMaskBase.color.a > 0)/*&& tTotal < timer*/)
        {
            colorTmp.a = fadeIn ? sRenderMaskBase.color.a + Time.deltaTime * factorFade : sRenderMaskBase.color.a - Time.deltaTime * factorFade;
            //colorTmp.a *= factorFade;

            sRenderMaskBase.color = colorTmp;
            tTotal += Time.deltaTime;

            yield return null;
        }

        isProcessing = false;

    }
}
