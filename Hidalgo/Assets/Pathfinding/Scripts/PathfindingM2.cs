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
using CodeMonkey.Utils;
using CodeMonkey;

public class PathfindingM2 : MonoBehaviour
{

    [SerializeField] private PathfindingDebugStepVisual pathfindingDebugStepVisual;
    [SerializeField] private PathfindingVisual pathfindingVisual;
    [SerializeField] private CharacterPathfindingMovementHandler characterPathfinding;
    private Pathfinding pathfinding;

    public int width = 20;
    public int height = 20;



    private void Awake()
    {
        pathfinding = new Pathfinding(width, height);
        pathfindingDebugStepVisual.Setup(pathfinding.GetGrid());
        pathfindingVisual.SetGrid(pathfinding.GetGrid());
    }

    public void SetNodeUnwalkable(Vector3 worldPositionNode)
    {
        pathfinding.GetGrid().GetXY(worldPositionNode, out int x, out int y);
        pathfinding.GetNode(x, y).SetIsWalkable(pathfinding.GetNode(x, y).isWalkable = false);
    }

    #region OLD
    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
    //        pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);

    //        List<PathNode> path = pathfinding.FindPath(0, 0, x, y);
    //        if (path != null)
    //        {
    //            for (int i = 0; i < path.Count - 1; i++)
    //            {
    //                Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, Color.green, 5f);
    //            }
    //        }
    //        characterPathfinding.SetTargetPosition(mouseWorldPosition);
    //    }


    //}
    #endregion
}
