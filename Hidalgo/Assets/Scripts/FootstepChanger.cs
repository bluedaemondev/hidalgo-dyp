using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepChanger : MonoBehaviour
{
    public AudioClip newFootstepSound;
    public Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.ChangeFootstepSound(newFootstepSound);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.RetrieveFootstepSound();
    }
}
