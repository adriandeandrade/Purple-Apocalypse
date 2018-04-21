using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected EntityData entityData; // Holds stuff like speed, attack speed, etc
    protected Animator animator;

    [SerializeField] protected float currentHealth;

    protected bool isAttacking;
    protected bool facingRight;
    protected bool isWalking;

    protected Vector2 direction;

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
        currentHealth = Health;
        facingRight = true;
        isWalking = false;
    }

    protected virtual void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        if (isWalking && !animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            transform.Translate(direction * MoveSpeed * Time.deltaTime);
        }
        AnimateMovement();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    protected void Die()
    {
        Destroy(gameObject);
    }

    protected void AnimateMovement()
    {
        if (isAttacking)
            return;

        if (direction != Vector2.zero)
        {
            animator.SetBool("isWalking", true);
            isWalking = true;
        }
        else
        {
            animator.SetBool("isWalking", false);
            isWalking = false;
        }

        if (direction.x > 0 && !facingRight || direction.x < 0 && facingRight && !isAttacking)
        {
            facingRight = !facingRight;
            Vector3 currentScale = transform.localScale;
            currentScale.x *= -1;
            transform.localScale = currentScale;
        }
    }
}
