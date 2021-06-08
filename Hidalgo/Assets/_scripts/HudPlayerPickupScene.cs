using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudPlayerPickupScene : MonoBehaviour
{
    public static HudPlayerPickupScene instance { get; private set; }

    public GameObject checkedRocinante;
    public GameObject checkedPieces;
    public GameObject checkedLast;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
    public void CheckRocinante()
    {

    }
}
