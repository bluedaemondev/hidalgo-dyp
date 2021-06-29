using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPathfinder
{
    void SetTarget(Vector3 positionToReach);
    void ClearPath();
}
