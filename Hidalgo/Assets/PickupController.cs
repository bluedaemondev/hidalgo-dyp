﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameSceneManagerPickupsLevel.instance.onPickupFuckingBullshit.Invoke();
        Destroy(this.gameObject);
    }
}
