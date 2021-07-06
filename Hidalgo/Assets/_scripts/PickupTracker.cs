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

    // avisos en pantalla para reforzar el feedback de que perdiste
    // un objeto
    public event Action<GameObject> onPickupRestored;
    // avisos en pantalla para reforzar el feedback de que perdiste
    // un objeto
    public event Action<GameObject> onPickupMissing;
    // cuando sueltan un pickup, todos recalculan distancia
    // si es menor, se van hasta ahi; sino, siguen a donde iban
    public event Action<Vector2> onPickupDropped;

    public Cinemachine.CinemachineTargetGroup group;

    public float weightPickupMissing = 2;

    public int totalPickups;


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        if (useChildren && transform.childCount > 0)
        {
            pickupsWOriginal.AddRange(transform.GetComponentsInChildren<Transform>().Select(t => t.gameObject).ToList());
        }

        totalPickups = pickupsWOriginal.Count;

        onPickupMissing += UpdateVisualsMissing;
        onPickupRestored += UpdateVisualsRestored;
    }

    public void PickupRecovered(GameObject pickup)
    {
        Debug.Log("Lost called " + totalPickups);
        totalPickups--;
        onPickupRestored.Invoke(pickup);
    }
    public void PickupLost(GameObject pickup)
    {
        Debug.Log("Lost called " + totalPickups);
        totalPickups--;
        onPickupMissing.Invoke(pickup);

        this.pickupsWOriginal.Remove(pickup);


        if (totalPickups <= 0)
        {
            HudWavesM2.instance.OnLose();
        }
    }



    void UpdateVisualsMissing(GameObject pickup)
    {
        //int groupMember = group.FindMember(pickup.transform);
        //var tmp = group.GetWeightedBoundsForMember(groupMember);
        /// TO DO:
        /// implementar cinemachine target group weight lerp
        /// moviendo el peso hasta 2, para que acompañe la camara mas de cerca

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

        //pickupsWOriginal.Find(p => pickupHierarchyName == p.name).SetActive(false);

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

        for (int i = 0; i < pickupsWOriginal.Count; i++)
        {
            Debug.Log("cuenta pickups  =" + pickupsWOriginal.Count);
            var distance = Vector2.Distance(pickupsWOriginal[i].transform.position, positionToCompare);
            if (distance <= comparer)
            {
                minDistanceIdx = i;
                comparer = distance;
            }
        }

        return pickupsWOriginal[minDistanceIdx].transform;

    }

    public void CallbackDropped(PickupController pickupInHand)
    {
        //Debug.Log("dropped pickup " + pickupInHand);
        pickupInHand.ResetPickupComponents(false, true);
        this.onPickupDropped(pickupInHand.transform.position);

    }
}
