using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleEntity : MonoBehaviour
{
    public void Destroy02()
    {
        Destroy(this.gameObject, 0.2f);
    }
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
