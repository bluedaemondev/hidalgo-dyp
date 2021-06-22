using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepChanger : MonoBehaviour
{
    public Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.OnWater = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.OnWater = false;
    }
}
