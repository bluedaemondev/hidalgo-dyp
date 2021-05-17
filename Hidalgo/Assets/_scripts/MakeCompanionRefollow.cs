using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCompanionRefollow : MonoBehaviour
{
    public LayerMask interactsWith;
    public Transform refolowTarget;
    public GameObject prefabParticlesMashButton;

    private bool hasCompanionInRange;

    [SerializeField] private int counterRequired = 6;
    private int counterCurrent = 0;

    [SerializeField] private Rocinante moverIa;
    [SerializeField] private KeyCode keyToReleaseCompanion = KeyCode.E;
    [SerializeField] private KeyCode secondaryKeyToReleaseCompanion = KeyCode.Space;

    private void Start()
    {
        if(refolowTarget == null)
        {
            refolowTarget = GameObject.FindObjectOfType<Player>().transform;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Common.GetLayersFromMask(interactsWith).Contains(collision.gameObject.layer))
        {
            hasCompanionInRange = true;
            moverIa = collision.GetComponent<Rocinante>();
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (hasCompanionInRange && moverIa.Follows != null)
        {
            if(Input.GetKeyDown(keyToReleaseCompanion) ||
                Input.GetKeyDown(secondaryKeyToReleaseCompanion))
            {
                EffectFactory.instance.InstantiateEffectAt(prefabParticlesMashButton, transform.position, Quaternion.identity);
                AddMashToCount();

            }
        }  
    }

    void AddMashToCount()
    {
        if(counterCurrent <= counterRequired)
        {
            this.counterCurrent++;
        }
        else
        {
            ReleaseInRange();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (hasCompanionInRange)
        {
            hasCompanionInRange = false;
        }
    }

    public void ReleaseInRange()
    {
        hasCompanionInRange = false;
        this.moverIa.Follows = refolowTarget;
        this.moverIa.GetComponent<AnimatedCharacterController>().State = CharacterState.MOVING;

    }
}
