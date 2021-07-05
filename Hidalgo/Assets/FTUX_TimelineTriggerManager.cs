using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FTUX_TimelineTriggerManager : MonoBehaviour
{
    public PlayableDirector FTUX_Timeline;
    public LayerMask layersToInteract;

    void Start()
    {
        FTUX_Timeline = GetComponent<PlayableDirector>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9 && HudPlayerPickupScene.instance.checkedRocinante.activeSelf)
        {
            FTUX_Timeline.Play();
        }
    }
}
