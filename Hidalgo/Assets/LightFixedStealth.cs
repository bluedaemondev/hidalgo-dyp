using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFixedStealth : MonoBehaviour
{
    public float timeSwitchState = 3f;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Switch());
    }

    // Update is called once per frame
    IEnumerator Switch()
    {
        while (true)
        {
            animator.SetBool("isActive", !animator.GetBool("isActive"));
            yield return new WaitForSeconds(timeSwitchState);
        }
    }
}
