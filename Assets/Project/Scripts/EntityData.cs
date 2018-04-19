using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEntityData", menuName = "Entity/Create New Entity Data")]
public class EntityData : ScriptableObject
{
    public float health;
    public float moveSpeed;
    public float attackSpeed;
    public float attackCooldown;
    public float attackDistance;
    public string _name;
    public string description;
}
