/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using V_AnimationSystem;
using CodeMonkey.Utils;
using System;

public class CharacterPathfindingMovementHandler : MonoBehaviour
{

    private const float speed = 40f;

    private int currentPathIndex;
    private List<Vector3> pathVectorList;

    public GameObject onPathSetEffect;

    public Action onStopMovingCallback;


    private void Update()
    {
        HandleMovement();
    }
    public void SetMovementPath(Vector3 worldPosition)
    {
        // efecto en donde va a ir el enemigo, para contabilizar prioridades
        EffectFactory.instance.InstantiateEffectAt(onPathSetEffect, worldPosition, Quaternion.identity);

        SetTargetPosition(worldPosition);
    }

    private void HandleMovement()
    {
        if (pathVectorList != null)
        {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            if (Vector3.Distance(transform.position, targetPosition) > 1f)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;

                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                //animatedWalker.SetMoveVector(moveDir);
                transform.position = transform.position + moveDir * speed * Time.deltaTime;
            }
            else
            {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count)
                {
                    StopMoving();
                    //animatedWalker.SetMoveVector(Vector3.zero);
                }
            }
        }
        // else {
        //    animatedWalker.SetMoveVector(Vector3.zero);
        //}
    }

    private void StopMoving()
    {
        pathVectorList = null;
        onStopMovingCallback?.Invoke();
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        currentPathIndex = 0;
        pathVectorList = Pathfinding.Instance.FindPath(GetPosition(), targetPosition);

        if (pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }
    }

}