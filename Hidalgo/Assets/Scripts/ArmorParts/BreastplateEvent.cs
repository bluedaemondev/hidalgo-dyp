using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreastplateEvent : MonoBehaviour
{
    public Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.ArmorState = 4f;
        player.SetArmorState();
    }
}
