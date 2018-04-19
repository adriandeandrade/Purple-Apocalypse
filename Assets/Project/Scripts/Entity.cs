using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Entity : MonoBehaviour, IDamageable
{
    [SerializeField] protected EntityData entityData;
    protected float currentHealth;
    protected bool isAttacking;

    public string Name
    {
        get
        {
            return entityData._name;
        }
    }
    public string Description
    {
        get
        {
            return entityData.description;
        }
    }
    public float MoveSpeed
    {
        get
        {
            return entityData.moveSpeed;
        }
    }
    public float Health
    {
        get
        {
            return entityData.health;
        }
    }
    public float AttackSpeed
    {
        get
        {
            return entityData.attackSpeed;
        }
    }
    public float AttackCooldown
    {
        get
        {
            return entityData.attackCooldown;
        }
    }
    public float AttackDistance
    {
        get
        {
            return entityData.attackDistance;
        }
    }

    public void TakeDamage(float damage)
    {
        throw new System.NotImplementedException();
    }
}
