using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineController : MonoBehaviour
{
    public LayerMask layersToInteract;

    bool hasRocinante;
    bool hasQuijote;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //var lstLayers = Common.GetLayersFromMask(layersToInteract);
        //if (!lstLayers.Contains(collision.gameObject.layer))
        //    return;

        if (collision.gameObject.layer == 9 /*LayerMask.GetMask("Player")*/)
            hasQuijote = true;
        else if (collision.gameObject.layer == 10/*LayerMask.GetMask("Companion")*/)
            hasRocinante = true;

        if (hasRocinante && collision.gameObject.layer == 9/*== LayerMask.GetMask("Player")*/ ||
            hasQuijote && collision.gameObject.layer == 10/*== LayerMask.GetMask("Companion")*/)
            PickupsScapeGameManager.instance.Win();

    }

}
