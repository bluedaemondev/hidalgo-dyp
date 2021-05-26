using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSpriteWithTarget : MonoBehaviour
{
    [SerializeField]
    Transform target;

    // Update is called once per frame
    void Update()
    {
        TurnSprite(target.position);
    }
    private void TurnSprite(Vector3 targetPos)
    {
        Vector3 aimDirection = (transform.position -target.position ).normalized;
        float angleRot = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angleRot);

        //return gunpoint.transform.eulerAngles;
        ////Debug.Log("Gunpoimt angle = " + angleRot);
    }

}
