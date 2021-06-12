using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    public GameObject entityToSpawn;

    public float radiusSpawn = 10f;
    public float timeSpawn = 4f;

    public Transform rotateAround;
    public bool useRotationPivot;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = GeneratePointBasedOnPivot(rotateAround.position, transform.position);
    }

    Vector2 GeneratePointBasedOnPivot(Vector2 positionPivot, Vector2 pointTransform)
    {
        Vector2 dir = new Vector2();

        var randX = Random.Range(0.1f, 1f);
        var randY = Random.Range(0.1f, 1f);

        var randPhase = Random.Range(-30, 31);

        var angle = Mathf.Atan(randY / randX) * Mathf.Rad2Deg + randPhase;

        var offset = (pointTransform - positionPivot).magnitude;

        Debug.Log(angle);

        dir.x =  Mathf.Tan(angle) * randX * offset - positionPivot.x ;
        dir.y =  Mathf.Tan(angle) * randY * offset - positionPivot.y;

        return dir;
    }
}
