﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PickupTracker : MonoBehaviour
{
    public static PickupTracker instance { get; private set; }


    public bool useChildren = true;
    public List<GameObject> pickupsWOriginal;

    public GameObject particlesRestoredItem;
    public GameObject particlesMissingItem;


    private List<GameObject> missingPickups;

    public event Action<GameObject> onPickupMissing;


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        if (useChildren && transform.childCount > 0)
        {
            pickupsWOriginal.AddRange(transform.GetComponentsInChildren<Transform>().Select(t => t.gameObject).ToList());
        }

        onPickupMissing += UpdateVisualsMissing;
    }

    void UpdateVisualsMissing(GameObject pickup)
    {
        EffectFactory.instance.InstantiateEffectAt(particlesMissingItem, pickup.transform.position, Quaternion.identity);
        EffectFactory.instance.camShake.ShakeCameraNormal(2, 0.75f);
    }
    void UpdateVisualsRestored(GameObject pickup)
    {
        EffectFactory.instance.InstantiateEffectAt(particlesRestoredItem, pickup.transform.position, Quaternion.identity);
        EffectFactory.instance.camShake.ShakeCameraNormal(1, 0.125f);
    }

    public void SetPickupMissing(GameObject pickup)
    {
        pickupsWOriginal.Remove(pickup);

        if (!missingPickups.Contains(pickup))
        {
            missingPickups.Add(pickup);
        }
    }
    public Transform GetRandomPickup()
    {
        int rand = UnityEngine.Random.Range(0, pickupsWOriginal.Count);

        return pickupsWOriginal[rand].transform;
    }


}