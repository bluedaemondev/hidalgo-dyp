using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FTUX_TimelineTriggerManager : MonoBehaviour
{
    public PlayableDirector FTUX_Timeline_Mercado;
    public PlayableDirector FTUX_Timeline_Cementerio;
    public LayerMask layersToInteract;

    void Start()
    {
        FTUX_Timeline_Mercado = GetComponent<PlayableDirector>();
        FTUX_Timeline_Cementerio = GetComponent<PlayableDirector>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9 && HudPlayerPickupScene.instance.checkedRocinante.activeSelf)
        {
            FTUX_Timeline_Mercado.Play();
        }

        else if (collision.gameObject.layer == 9 && collision.gameObject.layer == 13 && HudPlayerPickupScene.instance.checkedRocinante.activeSelf)
        {
            FTUX_Timeline_Cementerio.Play();
        }
    }
}
