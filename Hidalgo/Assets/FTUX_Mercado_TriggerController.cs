using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FTUX_Mercado_TriggerController : MonoBehaviour
{
    public PlayableDirector FTUX_Mercado;
    public LayerMask layersToInteract;

    void Start()
    {
        FTUX_Mercado = GetComponent<PlayableDirector>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9 && HudPlayerPickupScene.instance.checkedRocinante.activeSelf)
        {
            FTUX_Mercado.Play();
        }
    }
}
