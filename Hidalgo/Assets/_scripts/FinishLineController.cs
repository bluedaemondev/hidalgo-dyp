using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FinishLineController : MonoBehaviour
{
    public LayerMask layersToInteract;

    bool hasRocinante;
    bool hasQuijote;

    public GameObject uiShowMissingPickup;
    public GameObject uiShowWinPopup;
    public PlayableDirector timeline;

    void Start()
    {
        timeline = GetComponent<PlayableDirector>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //var lstLayers = Common.GetLayersFromMask(layersToInteract);
        //if (!lstLayers.Contains(collision.gameObject.layer))
        //    return;

        //if (collision.gameObject.layer == 9 /*LayerMask.GetMask("Player")*/)
        //    hasQuijote = true;
        //else if (collision.gameObject.layer == 10/*LayerMask.GetMask("Companion")*/)
        //    hasRocinante = true;

        //if (!hasRocinante || !PickupsScapeGameManager.instance.PickupsCompleted())
        //    uiShowMissingPickup.SetActive(true);

        //if (hasRocinante && collision.gameObject.layer == 9/*== LayerMask.GetMask("Player")*/ ||
        //    hasQuijote && collision.gameObject.layer == 10/*== LayerMask.GetMask("Companion")*/)
        //{
        //    PickupsScapeGameManager.instance.Win();
        //    timeline.Play();
        //}

        //FindObjectOfType<Rocinante>().IsAttachedToPlayer() && HudPlayerPickupScene.instance.checkedPieces.activeSelf
        if (HudPlayerPickupScene.instance.checkedRocinante.activeSelf && HudPlayerPickupScene.instance.checkedPieces.activeSelf)
        {
            timeline.Play();

            HudPlayerPickupScene.instance.CheckEscape();
        }
        // (!FindObjectOfType<Rocinante>().IsAttachedToPlayer() || !PickupsScapeGameManager.instance.PickupsCompleted())
        if (!HudPlayerPickupScene.instance.checkedRocinante.activeSelf || !HudPlayerPickupScene.instance.checkedPieces.activeSelf)
        {
            uiShowMissingPickup.SetActive(true);

            StartCoroutine(DisableUI());
        }

        IEnumerator DisableUI()
        {
            yield return new WaitForSeconds(4);
            uiShowMissingPickup.SetActive(false);
        }

    }
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    uiShowMissingPickup.SetActive(false);
    //    uiShowWinPopup.SetActive(false);
    //}

}
