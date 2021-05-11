﻿using System.Collections;
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

    public List<AudioClip> quijoteStepGrass1;

    public List<AudioClip> quijoteStepMud1;

    public List<AudioClip> rocinanteStepGrass1;

    public AudioClip quijoteComplaining1;

    public AudioClip rainAmbient;
    public AudioClip stormAmbient;
    public AudioClip thunderAmbient;

}
