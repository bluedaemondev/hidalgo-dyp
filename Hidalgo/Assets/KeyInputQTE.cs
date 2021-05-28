using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyCodeQTE { 
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

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!touched)
        {
            Instantiate(particlesMissed, transform.position, Quaternion.identity);
            Debug.Log("missed key");
            spriteRend.color = Color.black;
            SoundManager.instance.PlayEffect(PickupsScapeGameManager.instance.soundLibrary.wrongKey);
        }

        Destroy(this.gameObject);
    }

    private void OnTouched()
    {
        Debug.Log("ok tap");
        qteController.CurrentTotal++;
        

        Instantiate(particlesTouched, transform.position, Quaternion.identity);
        this.spriteRend.color = new Color(0, 0, 0, 0);

    }


}
