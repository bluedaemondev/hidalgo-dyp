using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnData : MonoBehaviour
{
    public List<GameObject> Enemies = new List<GameObject>();
    public float timeFromLastSpawn;

    public SpawnData(List<GameObject>newEnemy, float newSpawnTime)
    {
        Enemies = newEnemy;
        timeFromLastSpawn = newSpawnTime;
    }
  
}
