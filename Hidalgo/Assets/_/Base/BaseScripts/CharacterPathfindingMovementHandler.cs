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

    public float speed = 40f;
    public float speedMultiplier = 1;

    private int currentPathIndex;
    private List<Vector3> pathVectorList;

    public GameObject onPathSetEffect;

    public Action onStopMovingCallback;


    private void Update()
    {
        HandleMovement();
    }
    public void SetMovementPath(Vector3 worldPositionOrigin, Vector3 targetPosition)
    {
        Pathfinding.Instance.GetGrid().GetXY(worldPositionOrigin, out int x, out int y);
        Pathfinding.Instance.GetGrid().GetXY(targetPosition, out int xTarget, out int yTarget);

        Debug.Log("world position " + x + " " + y);
        Debug.Log("target position " + xTarget + " " + yTarget);

        List<PathNode> path = Pathfinding.Instance.FindPath(x, y, xTarget, yTarget);

        Debug.Log(path != null);

        if (path != null)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {
                Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, Color.green, 5f);
            }
        }


        // efecto en donde va a ir el enemigo, para contabilizar prioridades
        EffectFactory.instance.InstantiateEffectAt(onPathSetEffect, worldPositionOrigin, Quaternion.identity);

        SetTargetPosition(worldPositionOrigin);
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
                transform.position = transform.position + moveDir * speed * speedMultiplier * Time.deltaTime;
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