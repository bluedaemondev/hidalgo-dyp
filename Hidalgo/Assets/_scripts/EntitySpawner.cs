using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class EntitySpawner : MonoBehaviour
{
    public List<GameObject> entityToSpawn;

    public float radiusSpawn = 10f;
    public float timeSpawn = 4f;

    [Header("si queremos dar +/- tiempo entre spawn de enemigo por momentos")]
    public float timeSpawnModifier = 1f;

    public bool spawnActive = true;

    private Transform pivotPoint;

    private Func<Vector2> GenerateSpawnPosition;
    [SerializeField] private bool _usesRadius;

    public void SetTimerDoubleSpeed()
    {
        this.timeSpawnModifier = 0.5f;
    }
    public void ResetTimerSpeed()
    {
        this.timeSpawnModifier = 1f;
    }
    public void SetTimerSlowSpeed()
    {
        this.timeSpawnModifier = 1.5f;
    }


    // Start is called before the first frame update
    void Start()
    {
        pivotPoint = PickupTracker.instance.GetRandomPickup();

        if (_usesRadius)
            GenerateSpawnPosition = GenerateRandomPosInsideRadius;
        else
            GenerateSpawnPosition = GenerateAtSpawnerPosition;

        StartCoroutine(SpawnCyclic());
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Vector3.zero, radiusSpawn);
    }
    Vector2 GenerateRandomPosInsideRadius()
    {
        return Random.insideUnitCircle.normalized * radiusSpawn;
    }
    Vector2 GenerateAtSpawnerPosition()
    {
        return transform.position;
    }
    IEnumerator SpawnCyclic()
    {
        while (spawnActive && entityToSpawn != null)
        {
            int rRange = Random.Range(0, entityToSpawn.Count);
            var randPointRadius = GenerateSpawnPosition();

            //transform.position = randPointRadius;

            GameObject newEnemy = Instantiate(entityToSpawn[rRange], (Vector2)pivotPoint.position - randPointRadius, Quaternion.identity);
            newEnemy.transform.position = randPointRadius;
            // usar angulo de rotacion de los enemiogos para que miren al frente al instanciarlos

            if (newEnemy.TryGetComponent<Enemy_M2>(out Enemy_M2 m2))
            {
                m2.SetPickupTarget(PickupTracker.instance.GetRandomPickup().position);
            }

            yield return new WaitForSeconds(timeSpawn * timeSpawnModifier);
        }
    }

}
