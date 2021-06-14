using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimationOnTrigger : MonoBehaviour
{
    public Animator _animator;
    public string triggerName = "knocked";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _animator.SetTrigger(triggerName);
    }
}
