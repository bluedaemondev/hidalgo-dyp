using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAreaController : MonoBehaviour
{
    [SerializeField]
    private float _sumStunChance = 0;
    private List<IStunneable> _entitiesInArea;

    [Range(75, 1000f)]
    public float requiredChanceForStun = 100f;

    [Range(0.1f, 5f), Header("tiempo que queda stuneada la entidad")]
    public float stunForSeconds = 2f;
    [Header("Invertir el control en rayo")]
    public bool invertControlInPlayer = false;

    [Header("Prefab opcional con timer")]
    public GameObject prefabLightning;

    public ParticleSystem particlesRayosPlayer;


    private void Start()
    {
        foreach (var child in transform.GetComponentsInChildren<TriggerLightningAreaCounter>())
            child.InitTriggerZone(this);

        this._entitiesInArea = new List<IStunneable>();
    }

    /// <summary>
    /// Cualquiera de las entidades que sumen pasos va a generar chance de que caiga un rayo
    /// El rayo puede caer sobre cualquiera de las entidades que se pueden stunear, y se basa en un
    /// aleatorio de las entidades que sabe que estan en el area
    /// </summary>
    /// <param name="valueTime"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    public void SumStunChance(float valueTime, IStunneable entity)
    {
        if (!particlesRayosPlayer.gameObject.activeSelf)
            particlesRayosPlayer.gameObject.SetActive(true);

        this._sumStunChance += valueTime;
        this.AddEntity(entity);

        if (this._sumStunChance >= requiredChanceForStun)
        {
            this.ResetStunChance(true);

            /*var stun = */
            PickEntityToStun();

            // activar en el trigger del rayo

            //stun.GetStunned(this.stunForSeconds);
        }


    }
    public void AddEntity(IStunneable tmp)
    {
        if (!this._entitiesInArea.Contains(tmp))
            this._entitiesInArea.Add(tmp);
    }
    public void RemoveEntity(IStunneable tmp)
    {
        if (this._entitiesInArea.Contains(tmp))
            this._entitiesInArea.Remove(tmp);
    }

    private IStunneable PickEntityToStun()
    {
        int rand = Random.Range(0, this._entitiesInArea.Count);

        return this._entitiesInArea[rand];
    }


    public void ResetStunChance(bool ligtningOnReset = true)
    {
        if (ligtningOnReset)
        {
            GenerateLightningAt(PickEntityToStun());
        }

        this._sumStunChance = 0;
        particlesRayosPlayer.gameObject.SetActive(false);


    }

    public void GenerateLightningAt(IStunneable entity)
    {
        Vector2 positionToHit = ((MonoBehaviour)entity).transform.position;
        //if (prefabLightning != null)
        //{
        //    Instantiate(prefabLightning, positionToHit, Quaternion.identity);
        //}

        if (prefabLightning == null)
            ClimateController.instance.LightningEffect(positionToHit);
        else
            Instantiate(prefabLightning, entity.GetRigidbody().transform.position, Quaternion.identity);

        Debug.Log("Lightning! " + positionToHit + " || " + entity);
    }
}
