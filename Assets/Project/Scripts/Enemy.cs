//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Entity
{
    private Transform target;

    protected override void Start()
    {
        base.Start();
        currentHealth = entityData.health;
        isAttacking = false;
        target = FindObjectOfType<PlayerController>().transform;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Move()
    {
        Transform playerLocation = GetPlayerPosition();
        direction = (playerLocation.position - transform.position).normalized;
        base.Move();
    }

    private Transform GetPlayerPosition()
    {
        target = FindObjectOfType<PlayerController>().transform;
        return target;
    }


    //private bool WithinAttackDistance()
    //{
    //    float distance = Vector2.Distance(target.position, transform.position);
    //    if(distance > entityData.attackDistance)
    //    {
    //        return false;
    //    }

    //    return true;
    //}
}
