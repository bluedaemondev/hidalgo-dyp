using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FTUX_TimelineTriggerManager : MonoBehaviour
{
    public PlayableDirector FTUX_Timeline_Mercado;
    public PlayableDirector FTUX_Timeline_Cementerio;
    public LayerMask layersToInteract;

    bool FTUX_Timeline_Mercado_played = false;
    bool FTUX_Timeline_CementerioInicio_played = false;

    void Start()
    {
        FTUX_Timeline_Mercado = GetComponent<PlayableDirector>();
        FTUX_Timeline_Cementerio = GetComponent<PlayableDirector>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Play timeline Mercado inicio y final cuando DQ tiene a Rocinante
        if (collision.gameObject.layer == 9 && HudPlayerPickupScene.instance.checkedRocinante.activeSelf && FTUX_Timeline_Mercado_played == false)
        {
            FTUX_Timeline_Mercado.Play();
            FTUX_Timeline_Mercado_played = true;
        }

        //Play timeline Cementerio inicio cuando DQ tiene a Rocinante y pickup guantes
        else if (collision.gameObject.layer == 9 && collision.gameObject.layer == 13 && HudPlayerPickupScene.instance.checkedRocinante.activeSelf)
        {
            FTUX_Timeline_Cementerio.Play();
            FTUX_Timeline_CementerioInicio_played = true;
        }
    }
}
