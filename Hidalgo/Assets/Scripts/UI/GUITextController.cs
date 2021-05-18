using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUITextController : MonoBehaviour
{
    public float time = 5; //Seconds to read the text

    void Start()
    {
        Destroy(gameObject, time);
    }
}
