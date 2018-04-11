using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEntityData", menuName = "Entity/Create New Entity Data")]
public class LivingEntityData : ScriptableObject
{
    public float health;
    public float moveSpeed;
    public float attackSpeed;
    public float attackCooldown;
    public string _name;
    public string description;

}
