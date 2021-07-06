using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TriggerLightningAreaCounter : MonoBehaviour
{
    [Header("Optional")]
    [SerializeField] TilemapCollider2D _col;
    [Space, Header("Required")]
    [SerializeField] LightningAreaController _controller;
    [SerializeField] float _sumPerStep;

    public bool ResetOnExit = true;

    public void InitTriggerZone(LightningAreaController controller)
    {
        this._col = GetComponent<TilemapCollider2D>();
        //this._sumPerStep = sumPerStep;
        this._controller = controller;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!this._controller) { Debug.LogError("Lightning Area Controller is missing"); return; }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<IStunneable>() == null) { return; }

        var stun = collision.GetComponent<IStunneable>();
        
        if (!stun.IsStunned())
            _controller.SumStunChance(this._sumPerStep, stun);
        

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        var tmp = collision.GetComponent<IStunneable>();
        if (tmp == null || !ResetOnExit) { return; }

        _controller.ResetStunChance(false);
        _controller.RemoveEntity(tmp);
    }
}
