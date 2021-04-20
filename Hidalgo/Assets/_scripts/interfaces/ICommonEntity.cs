using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommonEntity
{
    Vector2 Move();
    void Idle();
    void SStun();
}
