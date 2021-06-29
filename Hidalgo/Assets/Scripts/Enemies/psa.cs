using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class psa : MonoBehaviour
{
    [SerializeField]
    private float speed = .5f;
    Vector3 start;
    Vector3 des;
    public Player player;
    Vector3 midPoint;

    void Start()
    {
        start = transform.position;
        des = player.transform.position;

        midPoint =  (des + transform.position) / 2;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, des, speed * Time.deltaTime);

        var deltaEnd = Vector2.Distance(transform.position, des);
        var deltaStart = Vector2.Distance(transform.position, start);
        var deltaMid= Vector2.Distance(transform.position, midPoint);

        //Si mi distancia a mid

        if (delta >)
        Debug.Log(midPoint);

    }
}
