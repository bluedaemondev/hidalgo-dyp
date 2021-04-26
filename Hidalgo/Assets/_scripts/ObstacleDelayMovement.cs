using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDelayMovement : MonoBehaviour
{
    [SerializeField] private float factorDelay = 0.35f;
    [SerializeField] private bool hasSomeoneInRange = false;
    [SerializeField] private LayerMask layersToInteract;

    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != Common.GetLayerFromMask(layersToInteract))
            return;


    }
}
