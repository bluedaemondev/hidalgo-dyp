using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttractLayerElements : MonoBehaviour
{
    [Header("Layer con objetos que quiero que sigan a este objeto")]
    public LayerMask layersToAttract;

    // tiempo que queda pendiente hasta perder de vista a la/las entidades que siguen
    public float tDelayAfterLost = 0.6f;
    //HACER! - anotar en tablon

    // objetos que estoy atrayendo
    private List<AnimatedCharacterController> currentlyInAgro;
    private Collider2D agroRadius;

    [SerializeField]
    private AnimatedCharacterController parentEntityController;

    [HideInInspector]
    public UnityEvent onEnterRange;

    [SerializeField] private HorseUIContainer uiRocinante;


    private void Awake()
    {
        onEnterRange = new UnityEvent();
    }
    void OnEnable()
    {
        agroRadius = this.GetComponent<Collider2D>();
        if (uiRocinante == null)
            uiRocinante = FindObjectOfType<HorseUIContainer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.layer == Common.GetLayerFromMask(layersToAttract))
        if (Common.GetLayersFromMask(layersToAttract).Contains(collision.gameObject.layer))
        {
            if (currentlyInAgro == null)
                currentlyInAgro = new List<AnimatedCharacterController>();

            var controllerObject = collision.GetComponent<AnimatedCharacterController>();

            //preguntar como hacer esto mejor en clase. no es lo mas limpio
            if (controllerObject.mover is Rocinante)
            {
                ((Rocinante)controllerObject.mover).Follows = transform;
                uiRocinante.SwitchState(true);
            }
            controllerObject.State = CharacterState.MOVING;
            if (!currentlyInAgro.Contains(controllerObject))
                currentlyInAgro.Add(controllerObject);

            Debug.Log(currentlyInAgro.Count + " entities in agro - " + this.gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Common.GetLayerFromMask(layersToAttract))
        {
            var controllerObject = collision.GetComponent<AnimatedCharacterController>();

            //fix para que priorizen seguir al jugador
            if (this.gameObject.layer != LayerMask.NameToLayer("ObstacleStun"))
            {
                if (controllerObject.mover is Rocinante && ((Rocinante)controllerObject.mover).Follows != GameObject.FindGameObjectWithTag("Player"))
                {
                    controllerObject.State = CharacterState.IDLE;
                    uiRocinante.SwitchState(false);


                    currentlyInAgro.Remove(controllerObject);
                }
            }
            Debug.Log(" entity out of agro - " + collision.gameObject.name);

        }
    }


}
