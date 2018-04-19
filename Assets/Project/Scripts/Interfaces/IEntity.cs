using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity
{
    void Move(Vector2 direction);
    void Attack(int damageToDeal);
}
