using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AreaPickupSceneController : MonoBehaviour
{
    public List<AreaPickupLockeable> areas;

    void Awake()
    {
        areas = FindObjectsOfType<AreaPickupLockeable>().ToList();
    }

    
}
