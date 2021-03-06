using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WaveItem
{
    public GameObject entityPrefab;
    public Vector3 spawnPoint;
    public Transform optionalSpawnpoint;
    public float timeFromLast;

    //public WaveItem(GameObject eprefab, Vector3 spawnP, float timeOffset, Transform optOrigin = null)
    //{
    //    this.entityPrefab = eprefab;
    //    optionalSpawnpoint = optOrigin;

    //    if (optOrigin != null)
    //    {
    //        this.spawnPoint = optOrigin.position;
    //    }
    //    else
    //    {
    //        this.spawnPoint = spawnP;
    //    }

    //    this.timeFromLast = timeOffset;
    //}

    public Vector3 GetSpawnPoint()
    {
        if (optionalSpawnpoint != null)
            return optionalSpawnpoint.position;
        else
            return spawnPoint;
    }

}

public class WaveSystem : MonoBehaviour
{
    public static WaveSystem instance { get; private set; }

    [Header("Escribir en orden cuantos espacios son de cada wave")]
    public int[] groupByCount;
    [Header("Contenedor de Spawn-EnemigoASpawnear para todas las waves, sin cortes")]
    public List<WaveItem> waveGroups;

    public event System.Action onSpawnEnemy;

    public event System.Action<int> onWaveFinishedSpawning;
    public event System.Action onEnemyDied;
    public event System.Action onLastWaveEnemyDied;

    private int totalEnemiesP;
    public int currentWave;


    private void LastEnemyDied(int waveLast)
    {
        Debug.Log("finalizando wave " + waveLast);
        if (waveLast < groupByCount.Length)
        {
            //StartCoroutine(SpawnGrouppedList(waveLast + 1));
            StartSpawning(waveLast);
        }
    }
    public void OnEnemyDied()
    {
        totalEnemiesP--;
        HudWavesM2.instance.OnEnemyKilled();

        if (totalEnemiesP <= 0)
        {
            LastEnemyDied(currentWave);
        }
    }


    private void Awake()
    {
        if (instance)
            Destroy(instance.gameObject);

        instance = this;

        /// Debugging => hace una ronda tras otra
        //onWaveFinishedSpawning +=
        //    (finishedWaveId) =>
        //    {
        //        Debug.Log("finalizando wave " + finishedWaveId);
        //        if (finishedWaveId < groupByCount.Length - 1)
        //            StartCoroutine(SpawnGrouppedList(finishedWaveId + 1));
        //    };
    }
    private void Start()
    {
        StartSpawning(currentWave);
    }


    /// <summary>
    /// Llamar con numero de wave (empezando en 0 como primer wave)
    /// </summary>
    /// <param name="idGroup">n Wave</param>
    public void StartSpawning(int idGroup = 0)
    {
        Debug.Log(idGroup);

        StartCoroutine(SpawnGrouppedList(idGroup));
        HudWavesM2.instance.OnNewWave(idGroup + 1);
        currentWave++;
    }


    private IEnumerator SpawnGrouppedList(int groupByCountId)
    {
        int countOffsetArray = 0;

        for (int x = 0; x < groupByCountId; x++)
        {
            countOffsetArray += groupByCount[x];
        }

        totalEnemiesP = groupByCount[groupByCountId];

        for (int idx = 0; idx < groupByCount[groupByCountId]; idx++)
        {
            yield return new WaitForSeconds(waveGroups[idx + countOffsetArray].timeFromLast);
            var entity = Instantiate(waveGroups[idx + countOffsetArray].entityPrefab, waveGroups[idx + countOffsetArray].GetSpawnPoint(), Quaternion.identity, transform).GetComponent<EnemyM2>();


            if (entity is ChaserEnemyM2)
            {
                entity.Init();
                var nearestPickup = PickupTracker.instance.GetNearestPickup(entity.transform.position);
                Debug.Log("ischaser " + entity.transform.position + " ----   " + nearestPickup.position);

                (entity as ChaserEnemyM2).SetPickupTarget(nearestPickup.position);
                var direction = (nearestPickup.position - transform.position).normalized;
                entity.transform.rotation = Quaternion.Euler(0, 0, direction.z);
            }
        }

        //onWaveFinishedSpawning(groupByCountId);
    }
    public int GetGroupRemainingTotal()
    {
        return groupByCount[currentWave];
    }
    public string GetRemainingEnemies()
    {
        return transform.childCount.ToString();
    }
}
