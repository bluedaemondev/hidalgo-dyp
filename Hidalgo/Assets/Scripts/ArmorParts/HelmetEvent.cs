using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetEvent : MonoBehaviour
{
    public GameObject ZonaRayos;
    public GameObject Wall1;
    public Player player;
    public List<GameObject> lsitCharcos;

    public PickupController controller;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PickupsScapeGameManager.instance.onPickupItem.Invoke(controller.Id);

        if (ZonaRayos != null)
        {
            ZonaRayos.SetActive(false);
            if (lsitCharcos.Count > 0)
                foreach (var chr in lsitCharcos)
                {
                    chr.SetActive(false);
                }
        }

        player.ArmorState = 3f;
        player.SetArmorState();

        if (Wall1 != null)
        {
            ClimateController.instance.LightningEffect(Wall1.transform.position, true);
            Destroy(Wall1);
        }
    }
}
