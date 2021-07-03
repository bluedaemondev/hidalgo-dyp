using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = .5f;
    [SerializeField]
    private float scaleSpeed = 1.5f;
    Vector3 start;
    Vector3 des;
    public Player player;
    Vector3 midPoint;
    Vector3 originalScale;
    [SerializeField]
    Vector3 maxScale;

    void Start()
    {
        start = transform.position;
        des = player.transform.position;
        originalScale = transform.localScale;
        midPoint = (des + transform.position) / 2;
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
            //GetComponent<BoxCollider>().enabled = false;
           // transform.localScale = Vector3.Lerp(originalScale, maxScale, scaleSpeed * Time.deltaTime);
        }
        Debug.Log(midPoint);

        //Si mi distancia a End es menor que mi distancia a Start bajo escala y activo collider.
        if (deltaEnd < deltaStart)
        {
            //GetComponent<BoxCollider>().enabled = true;
            //transform.localScale = Vector3.Lerp(maxScale, originalScale, scaleSpeed * Time.deltaTime);

        }
    }
}