using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable 
{
    Vector2 Move(Vector2 direction);
    Vector2 CalculateInput();

}
