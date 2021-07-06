using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreastplateEvent : MonoBehaviour
{
    public Player player;
    public PickupController controller;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PickupsScapeGameManager.instance.onPickupItem.Invoke(controller.Id);

        player.ArmorState = 4f;
        player.SetArmorState();

        HudPlayerPickupScene.instance.CheckPiecesComplete();
    }
}
