using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected EntityData entityData; // Holds stuff like speed, attack speed, etc
    protected float currentHealth;
    protected bool isAttacking;
    protected Vector2 direction;

    protected Animator animator;

    #region Properties
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
    #endregion

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        Move();
    }

    protected void Move()
    {
        transform.Translate(direction * MoveSpeed * Time.deltaTime);
        AnimateMovement();
    }

    public void TakeDamage(float damage)
    {
        throw new System.NotImplementedException();
    }

    protected void AnimateMovement()
    {
        if(direction.x != 0 && direction.y != 0)
        {
            animator.SetBool("isWalking", true);
        } else
        {
            animator.SetBool("isWalking", false);
        }
        
    }


}
