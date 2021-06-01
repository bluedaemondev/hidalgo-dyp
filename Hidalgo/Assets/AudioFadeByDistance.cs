using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFadeByDistance : MonoBehaviour
{
    private float distanceToTarget;
    [SerializeField]
    private float maxDistance;

    public List<AudioSource> affectedAudioSources;

    // Update is called once per frame
    void Update()
    {
        foreach (var source in affectedAudioSources)
        {
            distanceToTarget = Vector3.Distance(transform.position, source.transform.position);

            var clampedVol = Mathf.Clamp(1 - (distanceToTarget / maxDistance), 0, 1);

            source.volume = clampedVol;
            Debug.Log(clampedVol);
        }
    }
}
