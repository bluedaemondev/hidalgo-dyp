using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeClimateStateOnTrigger : MonoBehaviour
{
    public ClimateState changesTo;
    [SerializeField] private LayerMask interactsWith;
    
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ClimateController.instance.CurrentWeather = changesTo;
        Destroy(this.gameObject);
    }

}
