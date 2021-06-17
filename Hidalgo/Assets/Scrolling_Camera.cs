using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling_Camera : MonoBehaviour
{

    public float speed; 

    void Update()
    {

        transform.Translate(0, -speed, 0);
    }
}
