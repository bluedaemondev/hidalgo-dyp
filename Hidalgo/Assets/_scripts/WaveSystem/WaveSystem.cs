using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WaveItem
{
    public GameObject entityPrefab;
    public Vector3 spawnPoint;
    public float timeFromLast;

    public WaveItem(GameObject eprefab, Vector3 spawnP, float timeOffset)
    {
        this.entityPrefab = eprefab;
        this.spawnPoint = spawnP;
        this.timeFromLast = timeOffset;
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

    [Space, Header("Para mostrar enemigos restantes de la oleada")]
    public TMPro.TextMeshProUGUI textStatusRemaining;

    private void Awake()
    {
        if (instance)
            Destroy(instance.gameObject);

        instance = this;

        onWaveFinishedSpawning +=
            (finishedWaveId) =>
            {
                Debug.Log("finalizando wave " + finishedWaveId);
                if (finishedWaveId < groupByCount.Length - 1)
                    StartCoroutine(SpawnGrouppedList(finishedWaveId + 1));
            };

        StartSpawning();
    }


    /// <summary>
    /// Llamar con numero de wave (empezando en 0 como primer wave)
    /// </summary>
    /// <param name="idGroup">n Wave</param>
    public void StartSpawning(int idGroup = 0)
    {
        StartCoroutine(SpawnGrouppedList(idGroup));

        try
        {
            textStatusRemaining.text = "Enemigos restantes: " + groupByCount[idGroup];
        }
        catch { }
    }


    private IEnumerator SpawnGrouppedList(int groupByCountId)
    {
        int countOffsetArray = 0;
        for (int x = 0; x < groupByCountId; x++)
        {
            countOffsetArray += groupByCount[x];
        }
        for (int idx = countOffsetArray; idx < groupByCount[groupByCountId]; idx++)
        {
            yield return new WaitForSeconds(waveGroups[idx].timeFromLast);
            var entity = Instantiate(waveGroups[idx].entityPrefab, waveGroups[idx].spawnPoint, Quaternion.identity, transform);

            // completar con el merge despues
            //entity.GetComponent<ISpawneable>().Init();
        }

        onWaveFinishedSpawning(groupByCountId);
    }

}
