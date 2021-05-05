using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// para cada una de las zonas que haya con restriccion de tipo
//  "necesito x objeto para acceder a esta zona"
/// deberia estar en cada area/region del mapa
/// </summary>
/// 
[RequireComponent(typeof(Collider2D))]
public class AreaPickupLockeable : MonoBehaviour
{
    [SerializeField] private string neededIdPickup = "1";

    [SerializeField] private bool isUnlocked = false;
    public ParticleSystem areaParticlesRefference;


    public event Action onUnlockArea;


    public void UnlockArea(string idPickup)
    {
        if (this.isUnlocked || idPickup != neededIdPickup)
            return;

        this.isUnlocked = true;
        if (onUnlockArea != null)
        {
            onUnlockArea();
        }
    }

    private void PlayClipUnlock()
    {
        SoundManager.instance.PlayAmbient(GameSceneManagerPickupsLevel.instance.soundLibrary.unlockZoneAlert);
    }
    private void DisableCollider()
    {
        this.GetComponent<Collider2D>().enabled = false;
    }
    private void ParticlesUnlock()
    {
        areaParticlesRefference.emission.SetBurst(0, new ParticleSystem.Burst(0, 14, 20));
        areaParticlesRefference.Stop();
    }

    // to do : agregar transicion / feedback de que se desbloqueo la zona con algun efecto visual y de sonido
    void Start()
    {
        GameSceneManagerPickupsLevel.instance.onPickupItem.AddListener(this.UnlockArea);
        onUnlockArea += PlayClipUnlock;
        onUnlockArea += DisableCollider;
        onUnlockArea += ParticlesUnlock;
    }

}
