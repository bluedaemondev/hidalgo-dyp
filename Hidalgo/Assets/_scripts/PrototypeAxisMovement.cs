using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeAxisMovement : MonoBehaviour
{
    public float speed = 2.6f;

    void Update()
    {
        var horizontalAxis = Input.GetAxis("Horizontal");
        var verticalAxis = Input.GetAxis("Vertical");

        if (horizontalAxis != 0 || verticalAxis != 0)
        {
            transform.position += Time.deltaTime * speed * new Vector3(horizontalAxis, verticalAxis);
        }
    }
}
