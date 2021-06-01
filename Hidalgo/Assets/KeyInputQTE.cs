using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyCodeQTE
{
    W = KeyCode.W,
    S = KeyCode.S,
    A = KeyCode.A,
    D = KeyCode.D
}

public class KeyInputQTE : MonoBehaviour
{
    [Header("Tecla a pulsar")]
    public KeyCodeQTE requiredKey;

    private bool touched;

    private SpriteRenderer spriteRend;

    public GameObject particlesTouched;
    public GameObject particlesMissed;

    [SerializeField]
    private QuickTimeEventController qteController;

    //event Action onTouched;

    // Start is called before the first frame update
    void Start()
    {
        this.touched = false;
        this.spriteRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown((KeyCode)requiredKey) && !touched)
        {
            touched = true;
            OnTouched();
        }
        else if (Input.anyKeyDown && !Input.GetKeyDown((KeyCode)requiredKey) && !touched)
        {
            Debug.Log(collision.gameObject.name);
            Debug.Log("exit call");

            //touched = true;
            OnWrong();
        }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!touched)
        {
            touched = false;
            OnWrong();
        }

        Destroy(this.gameObject);
    }

    private void OnWrong()
    {
        //qteController.CurrentTotal += 0;
        Instantiate(particlesMissed, transform.position, Quaternion.identity);
        Debug.Log("missed key");
        spriteRend.color = Color.black;
        SoundManager.instance.PlayEffect(PickupsScapeGameManager.instance.soundLibrary.wrongKey);
    }
    private void OnTouched()
    {
        Debug.Log("ok tap");
        qteController.CurrentTotal++;


        Instantiate(particlesTouched, transform.position, Quaternion.identity);
        this.spriteRend.color = new Color(0, 0, 0, 0);

    }


}
