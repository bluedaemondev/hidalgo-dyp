using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeControls : MonoBehaviour
{
    public Player _player;
    public BoxCollider2D myBoxCollider;
    public float ChangeControlsBackTime = 0.75f;
    public float ChangeControlsTime = 1.7f;
    public float NormalSpeed;
    public float StunTime = 50;

    private void Awake()
    {
        myBoxCollider = this.GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(ChangeControlOnEntry());
    }   

    private void OnTriggerExit2D(Collider2D collision)
    {        
        StartCoroutine(StunOnExit());
    }

    public IEnumerator StunOnExit()
    {
        _player.speed = 0;
        yield return new WaitForSeconds(StunTime);
        

        StartCoroutine(ChangeControlOnExit());
       
     
    }

    public IEnumerator ChangeControlOnEntry()
    {
        yield return new WaitForSeconds(ChangeControlsTime);
        _player.speed = _player.speed * 0.75f;
        _player.ChangeMyController();

    }

    public IEnumerator ChangeControlOnExit()
    {
        yield return new WaitForSeconds(ChangeControlsBackTime);
        _player.speed = NormalSpeed;
        _player.RetrieveMyController();
    }
}
