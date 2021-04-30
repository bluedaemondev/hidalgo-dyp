using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerLevelSelection : MonoBehaviour
{
    //public string sceneLoadName = "";
    //private UnityEvent OnInteraction;
    [Header("Numero de nivel asociado - 1:Topdown")]
    public Enums.Level idLevel;
    bool canBeActivated = true;

    public LayerMask interactsWith;
    private KeyCode interactKeyPrimary = KeyCode.E;
    private KeyCode interactKeySecondary = KeyCode.Space;

    private Managers.HubGameManager _gameManager;
    private void Awake()
    {
        if (!_gameManager)
            _gameManager = FindObjectOfType<Managers.HubGameManager>();

        //if (OnInteraction == null)
        //    OnInteraction = new UnityEvent();

        //OnInteraction.AddListener(TestMethod);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!Common.GetLayersFromMask(interactsWith).Contains(collision.gameObject.layer))
            return;


        if ((Input.GetKeyDown(interactKeyPrimary) || Input.GetKeyDown(interactKeySecondary)) && canBeActivated)
        {
            canBeActivated = false;
            TestMethod();
            //OnInteraction?.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        ResetTrigger();
    }

    /// <summary>
    /// llamar despues de que se cierra el popup del about de nivel
    /// si es que elige quedarse en la escena / cerrarlo
    /// </summary>
    public void ResetTrigger()
    {
        this.canBeActivated = true;
    }

    void TestMethod()
    {
        Debug.Log("On interaction callback");
        _gameManager.LoadScene(this.idLevel);
        // show popup ( about the scene )
        // freeze counters to lock the player static
     
    }
}
