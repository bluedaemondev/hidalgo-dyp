using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlovesEvent : MonoBehaviour
{
    public Player player;
    public GameObject Wall;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.ArmorState = 2f;
        player.SetArmorState();

        if(Wall != null)
        {
            ClimateController.instance.LightningEffect(Wall.transform.position);
            Destroy(Wall);
        }
    }
}
