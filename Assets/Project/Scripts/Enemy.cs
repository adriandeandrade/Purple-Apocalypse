using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : LivingEntity
{
    private float waitTime = 0.5f;
    private Transform target;
    private bool isInRange = false;
    private bool isScreaming;
    [SerializeField] private LayerMask targetMask;
    private EnemyStates currentState = EnemyStates.IDLE;

    private void Start()
    {
        currentHealth = entityData.health;
        isScreaming = false;
        isAttacking = false;
        target = FindObjectOfType<PlayerController>().transform;
    }

    private void Update()
    {
        SwitchStates();
    }

    void SwitchStates()
    {
        switch(currentState)
        {
            case EnemyStates.IDLE:
                break;
            case EnemyStates.ATTACK:
                break;
            case EnemyStates.WALK:
                break;
            case EnemyStates.SCREAM:
                break;
        }
    }

    IEnumerator Attack()
    {
        
        yield return new WaitForSeconds(entityData.attackCooldown);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController target = other.GetComponent<PlayerController>();
        if(target != null)
        {
            currentState = EnemyStates.ATTACK;
            Debug.Log("ATTACKING");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerController target = other.GetComponent<PlayerController>();
        if (target != null)
        {
            currentState = EnemyStates.IDLE;
            Debug.Log("IDLEING");
        }
    }

    void LocatePlayer()
    {
        Vector2 pointA, pointB;
        pointA = transform.position;
        pointB.x = transform.position.x + 0.62f;
        pointB.y = transform.position.y + 1.04f;
        Collider2D[] targets = Physics2D.OverlapAreaAll(pointA, pointB, targetMask);
        if(targets.Length > 0)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                // DEAL DAMAGE TO TARGETS
                Debug.Log("Damage dealt");
            }
        }
        Debug.Log("RAN");
    }

    protected override void Movement()
    {
        Vector2 direction = (target.position - transform.position).normalized;
    }
}

public enum EnemyStates
{
    WALK,
    IDLE,
    SCREAM,
    ATTACK
}
