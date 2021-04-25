using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractLayerElements : MonoBehaviour
{
    [Header("Layer con objetos que quiero que sigan a este objeto")]
    public LayerMask layersToAttract;

    // tiempo que queda pendiente hasta perder de vista a la/las entidades que siguen
    public float tDelayAfterLost = 0.6f;
    //HACER! - anotar en tablon

    // objetos que estoy atrayendo
    private List<AnimatedCharacterController> currentlyInAgro;
    private CircleCollider2D agroRadius;

    [SerializeField]
    private AnimatedCharacterController parentEntityController;


    void OnEnable()
    {
        agroRadius = this.GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Common.GetLayerFromMask(layersToAttract))
        {
            if (currentlyInAgro == null)
                currentlyInAgro = new List<AnimatedCharacterController>();

            var controllerObject = collision.GetComponent<AnimatedCharacterController>();
            controllerObject.mover.SetTargetToFollow(transform);
            controllerObject.State = CharacterState.MOVING;

            currentlyInAgro.Add(controllerObject);
            Debug.Log(currentlyInAgro.Count + " entities in agro - " + this.gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Common.GetLayerFromMask(layersToAttract))
        {
            var controllerObject = collision.GetComponent<AnimatedCharacterController>();
            controllerObject.State = CharacterState.IDLE;

            currentlyInAgro.Remove(controllerObject);
            Debug.Log(" entity out of agro - " + collision.gameObject.name);

            

        }
    }

    
}
