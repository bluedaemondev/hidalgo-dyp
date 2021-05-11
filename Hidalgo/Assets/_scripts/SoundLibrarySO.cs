using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Guarda todos los clips de audio que parezcan necesarios para la escena
/// </summary>
[CreateAssetMenu(fileName ="Sound Library", menuName ="Sound Library", order = 0)]
public class SoundLibrarySO : ScriptableObject
{
    public AudioClip unlockZoneAlert;
    // agregar los sonidos generales de escena

    public AudioClip quijoteStepGrass1;
    public AudioClip quijoteStepGrass2;
    public AudioClip quijoteStepGrass3;

    public AudioClip quijoteStepMud1;

    public AudioClip rocinanteStepGrass1;
    public AudioClip rocinanteStepGrass2;
    public AudioClip rocinanteStepGrass3;

    public AudioClip quijoteComplaining1;

}
