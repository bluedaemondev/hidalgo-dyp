using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PickupTracker : MonoBehaviour
{
    public static PickupTracker instance { get; private set; }

    public Transform gridPivotPoint;

    public bool useChildren = false;
    public List<GameObject> pickupsWOriginal;

    public GameObject particlesRestoredItem;
    public GameObject particlesMissingItem;

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

    public void SetPickupMissing(string pickupHierarchyName)
    {
        pickupsWOriginal.Find(p => pickupHierarchyName == p.name).SetActive(false);

        //if (!missingPickups.Contains(pickup))
        //{
        //    missingPickups.Add(pickup);
        //}
    }
    public Transform GetRandomPickup()
    {
        int rand = UnityEngine.Random.Range(0, pickupsWOriginal.Count);

        return pickupsWOriginal[rand].transform;
    }

    /// <summary>
    /// Hace la distancia mas corta en linea recta 
    /// </summary>
    /// <param name="positionToCompare">posicion del enemigo que agarra el pickup</param>
    /// <returns></returns>
    public Transform GetNearestPickup(Vector2 positionToCompare)
    {
        int minDistanceIdx = 0;
        float comparer = 9999999999;

        for (int i = 0; i< pickupsWOriginal.Count; i++)
        {
            var distance = Vector2.Distance(pickupsWOriginal[i].transform.position, positionToCompare);
            if(distance <= comparer)
            {
                minDistanceIdx = i;
                comparer = distance;
            }
        }

        return pickupsWOriginal[minDistanceIdx].transform;

    }


}
