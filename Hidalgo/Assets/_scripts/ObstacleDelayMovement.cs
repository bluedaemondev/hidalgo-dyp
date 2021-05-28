using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDelayMovement : MonoBehaviour
{
    [SerializeField] private float factorDelay = 0.35f;
    [SerializeField] private bool hasSomeoneInRange = false;
    [SerializeField] private LayerMask layersToInteract;

    MovementType movement;

    public MovementType Movement { get => movement; private set => movement = value; }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Common.GetLayersFromMask(layersToInteract).Contains(collision.gameObject.layer))
            return;

        movement = collision.gameObject.GetComponent<MovementType>();

        if (movement is Player)
            ((Player)movement).SetSpeedMultiplier(factorDelay);
        else if (movement is Rocinante)
            ((Rocinante)movement).SetSpeedMultiplier(factorDelay);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!Common.GetLayersFromMask(layersToInteract).Contains(collision.gameObject.layer))
            return;

        movement = collision.gameObject.GetComponent<MovementType>();

        if (movement is Player)
            ((Player)movement).ResetSpeedMultipliers();
        else if (movement is Rocinante)
            ((Rocinante)movement).ResetSpeedMultipliers();

    }
}
