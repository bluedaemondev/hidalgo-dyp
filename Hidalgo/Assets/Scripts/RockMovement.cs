using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = .5f;
    [SerializeField]
    private float scaleSpeed = 1.5f;
    [SerializeField]
    GameObject player;
    Vector3 start;
    Vector3 des;
    Vector3 midPoint;
    Vector3 originalScale;
    [SerializeField]
    Vector3 maxScale;
    [SerializeField]
    GameObject fallArea;
    Animator myAnimator;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        start = transform.position;
        des = player.transform.position;
        originalScale = transform.localScale;
        midPoint = (des + transform.position) / 2;
        Instantiate(fallArea, des, Quaternion.identity);
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        //transform.position = Vector3.Lerp(transform.position, des, speed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, des, speed * Time.deltaTime);

        var deltaEnd = Vector2.Distance(transform.position, des);
        var deltaStart = Vector2.Distance(transform.position, start);
        var deltaMid = Vector2.Distance(transform.position, midPoint);

        //Si mi distancia a mid es menor a mi distancia a End subo escala y desactivo collider.

        if (deltaMid < deltaEnd)
        {
            
            GetComponent<BoxCollider>().enabled = false;
            //transform.localScale = Vector3.Lerp(originalScale * 1.5f, originalScale, scaleSpeed * Time.deltaTime);
        }
        Debug.Log(midPoint);

        //Si mi distancia a End es menor que mi distancia a Start bajo escala y activo collider.
        if (deltaEnd < deltaStart)
        {
            myAnimator.SetTrigger("IsMid");

            GetComponent<BoxCollider>().enabled = true;
            //transform.localScale = Vector3.Lerp(originalScale, originalScale * 1.5f, scaleSpeed * Time.deltaTime);

        }
    }
}