using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetOnUpdate : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
     public Transform targetFollow;


    [Header("Pisar el offset entre el objeto y target al iniciar"),
        SerializeField]
    private bool autoSnapToTargetOnStart;

    [Range(1, 12)]
    public float factorSmoothFollow = 0.8f;


    void Start()
    {
        this.offset = Vector2.zero;

        if (!autoSnapToTargetOnStart)
            this.offset = transform.position - targetFollow.position;
    }

    // en update de camara y animaciones
    void LateUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, targetFollow.position + offset, factorSmoothFollow * Time.deltaTime);
    }
}
