using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLightningAreaCounter : MonoBehaviour
{
    [Header("Optional")]
    [SerializeField] PolygonCollider2D _col;
    [SerializeField] LightningAreaController _controller;
    [Space, Header("Required")]
    [SerializeField] float _sumPerStep;

    public bool ResetOnExit = true;

    public void InitTriggerZone(/*float sumPerStep,*/ LightningAreaController controller)
    {
        this._col = GetComponent<PolygonCollider2D>();
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
