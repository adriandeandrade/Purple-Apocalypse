//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Entity
{
    private Transform target;
    private float attackCooldown;

    private 

    protected override void Start()
    {
        base.Start();
        attackCooldown = entityData.attackCooldown;
        currentHealth = entityData.health;
        isAttacking = false;
        target = FindObjectOfType<PlayerController>().transform;
    }

    protected override void Update()
    {
        base.Update();
        

        if(WithinAttackDistance())
        {
            if(attackCooldown <= 0)
            {
                attackCooldown = entityData.attackCooldown;
                StartCoroutine(Attack(entityData.attackCooldown / 2));
                Debug.Log("Attacked");
            }

            attackCooldown -= Time.deltaTime;
        }
    }

    protected override void Move()
    {
        Transform playerLocation = GetPlayerPosition();
        direction = (playerLocation.position - transform.position).normalized;
        bool isCloseEnough = WithinAttackDistance();
        if (!isCloseEnough)
        {
            base.Move();
        }
    }

    private Transform GetPlayerPosition()
    {
        target = FindObjectOfType<PlayerController>().transform;
        return target;
    }


    private bool WithinAttackDistance()
    {
        float distance = Vector2.Distance(target.position, transform.position);
        if (distance > entityData.attackDistance)
        {
            return false;
        }

        direction = Vector2.zero;
        animator.SetBool("isWalking", false);

        return true;
    }
}
