using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectFactory : MonoBehaviour
{
    public static EffectFactory instance { get; private set; }
    void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(this);
    }

    public void InstantiateEffectAt(GameObject prefabEffect, Vector2 position, Quaternion rotation, Transform parent = null)
    {
        GameObject spwn = Instantiate(prefabEffect, position, rotation, parent);
        if (spwn.GetComponent<ParticleSystem>())
        {
            var pSys = spwn.GetComponent<ParticleSystem>();

            pSys.Play();
            //pSys.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            Destroy(pSys.gameObject, pSys.main.duration);

        }
    }
}
