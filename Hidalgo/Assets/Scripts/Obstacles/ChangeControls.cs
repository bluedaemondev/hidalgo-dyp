using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeControls : MonoBehaviour
{
    public Player _player;
    //public BoxCollider2D myBoxCollider;
    public float ChangeControlsBackTime = 0.75f;
    public float ChangeControlsTime = 1.7f;
    public float NormalSpeed;
    public float StunTime = 50;

    public LayerMask interactWith;

    public MaskControlChangerController maskController;

    //private void Awake()
    //{
    //    myBoxCollider = this.GetComponent<BoxCollider2D>();
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Common.GetLayersFromMask(interactWith).Contains(collision.gameObject.layer))
        {
            StartCoroutine(ChangeControlOnEntry());
            Debug.Log("collision - " + collision.gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Common.GetLayersFromMask(interactWith).Contains(collision.gameObject.layer))
        {
            //StartCoroutine(StunOnExit());
            Debug.Log("deprecated method. Gonza, revisa esto.");
            Debug.Log("collision - " + collision.gameObject.name);

        }
    }

    public IEnumerator StunOnExit()
    {
        _player.SetSpeedMultiplier(0); //Speed = 0;
        yield return new WaitForSeconds(StunTime);

        StartCoroutine(ChangeControlOnExit());

    }

    public IEnumerator ChangeControlOnEntry()
    {
        maskController?.FadeInMask(ChangeControlsTime);
        yield return null;

        yield return new WaitForSeconds(ChangeControlsTime);
        //_player.Speed = _player.Speed * 0.75f;
        _player.ChangeMyController();

    }

    public IEnumerator ChangeControlOnExit()
    {
        maskController?.FadeOutMask(ChangeControlsTime);
        yield return null;

        yield return new WaitForSeconds(ChangeControlsBackTime);

        _player.ResetSpeedMultipliers();
        _player.RetrieveMyController();
    }
}
