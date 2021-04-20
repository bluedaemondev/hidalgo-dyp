using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTopDown : MonoBehaviour
{
    [SerializeField]
    CharacterEntity _entity;
    [SerializeField]
    Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        if (_entity == null)
        {
            _entity = new CharacterEntity(8);
        }
    }

    // Update is called once per frame
    void Update()
    {
        _entity.Update();
    }
}
