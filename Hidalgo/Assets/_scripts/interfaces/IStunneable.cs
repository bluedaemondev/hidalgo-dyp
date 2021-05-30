using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStunneable 
{
    void GetStunned(float timeStun);
    void Destun();
    IEnumerator CancelMovementInputFor(float time);
    bool IsStunned();

    Rigidbody2D GetRigidbody();

}
