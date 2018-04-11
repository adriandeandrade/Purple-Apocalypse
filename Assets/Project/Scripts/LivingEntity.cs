using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class LivingEntity : MonoBehaviour
{

    [SerializeField] private LivingEntityData entityData;
    protected float currentHealth;

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

    protected virtual void TakeDamage()
    {

    }

    protected virtual void Movement(float _moveSpeed)
    {
        //transform.Translate()
    }
}
