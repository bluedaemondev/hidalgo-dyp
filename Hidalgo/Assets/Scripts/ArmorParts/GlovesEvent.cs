using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlovesEvent : MonoBehaviour
{
    public Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.ArmorState = 2f;
        player.SetArmorState();
    }
}
