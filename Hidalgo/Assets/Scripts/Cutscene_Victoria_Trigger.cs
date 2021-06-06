using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Cutscene_Victoria_Trigger : MonoBehaviour
{
    public PlayableDirector timeline;

    void Start()
    {
        timeline = GetComponent<PlayableDirector>();
    }

    void OnTriggerExit (Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            timeline.Stop();
        }
    }

    void OnTriggerEnter (Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            timeline.Play();
        }
    }
}
