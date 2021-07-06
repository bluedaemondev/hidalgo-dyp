using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSpriteWithTarget : MonoBehaviour
{
    [SerializeField]
    Transform target;
    public bool useMouse;

    private System.Action RotateHandler;

    private void Awake()
    {
        if (useMouse)
            RotateHandler = TurnSpriteMouse;
        else
            RotateHandler = TurnSprite;
    }

    // Update is called once per frame
    void Update()
    {
        RotateHandler();
    }

    private void TurnSprite()
    {
        Vector3 aimDirection = (target.position- transform.position).normalized;
        float angleRot = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angleRot);

        //return gunpoint.transform.eulerAngles;
        ////Debug.Log("Gunpoimt angle = " + angleRot);
    }
    private void TurnSpriteMouse()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var dir = (transform.position - pos).normalized;

        float angleRot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angleRot);
    }

}
