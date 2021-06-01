using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetEvent : MonoBehaviour
{
    public GameObject ZonaRayos;
    public GameObject Wall1;

    public List<GameObject> lsitCharcos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ZonaRayos != null)
        {
            ZonaRayos.SetActive(false);
            foreach(var chr in lsitCharcos)
            {
                chr.SetActive(false);
            }
        }

        if(Wall1 != null)
        {
            ClimateController.instance.LightningEffect(Wall1.transform.position);
            Destroy(Wall1);
        }
    }
}
