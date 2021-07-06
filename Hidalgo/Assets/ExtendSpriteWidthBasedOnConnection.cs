using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendSpriteWidthBasedOnConnection : MonoBehaviour
{
    public Transform targetTransform;
    public float maxDistanceWidth;


    public SpriteRenderer sprRendToGrow;

    private Vector2 startPoint;

    void UpdateRopeGraphic(Vector2 newPos)
    {

        //transform.position = newPos;
        var direction = startPoint - newPos;
        transform.right = direction * transform.lossyScale.x;

        float dist = Vector2.Distance(startPoint, newPos);
        sprRendToGrow.size = new Vector2(dist, sprRendToGrow.size.y);
    }

    private void OnEnable()
    {
        startPoint = transform.position;
        UpdateRopeGraphic(targetTransform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //if(Vector2.Distance(transform.position, targetTransform.position) <= maxDistanceWidth)
        //{
            Debug.Log("in range");
            UpdateRopeGraphic(targetTransform.position);
        //}
    }
}
