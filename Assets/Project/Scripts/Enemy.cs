//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Entity
{
    private Transform target;

    private float waitTime = 0.5f;
    private bool isInRange = false;
    private bool isScreaming;
    
    private EnemyStates currentState = EnemyStates.IDLE;
    private Animator animator;

    private void Start()
    {
        currentHealth = entityData.health;
        isScreaming = false;
        isAttacking = false;
        target = FindObjectOfType<PlayerController>().transform;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(!isScreaming && !isAttacking)
            StartCoroutine(RandomScream());

        if(!isAttacking && !isScreaming)
        {
            if (WithinAttackDistance())
            {
                isAttacking = true;
                Debug.Log("Is within attacking distance");
                currentState = EnemyStates.ATTACK;
                SwitchStates();
            }
        }
    }

    void SwitchStates()
    {
        switch(currentState)
        {
            case EnemyStates.IDLE:
                break;
            case EnemyStates.ATTACK:
                StartCoroutine(Attack());
                break;
            case EnemyStates.WALK:
                //Move();
                break;
            case EnemyStates.SCREAM:
                break;
        }
    }

    IEnumerator Attack()
    {
        Debug.Log("Attacked!");
        animator.SetTrigger("attack");
        yield return new WaitForSeconds(entityData.attackCooldown);
        isAttacking = false;
        currentState = EnemyStates.WALK;
        SwitchStates();
    }

    IEnumerator RandomScream()
    {
        float rand = Random.Range(0, 100);
        if(rand < 90 && !isScreaming)
        {
            animator.SetTrigger("scream");
            isScreaming = true;
            Debug.Log("Screamed");  
            yield return new WaitForSeconds(2f);
        }
        isScreaming = false;
    }

    private bool WithinAttackDistance()
    {
        float distance = Vector2.Distance(target.position, transform.position);
        if(distance > entityData.attackDistance)
        {
            return false;
        }

        return true;
    }

    public void Move(Vector2 direction)
    {
        //Vector2 direction = (target.position - transform.position).normalized;
        transform.Translate(direction * entityData.moveSpeed * Time.deltaTime);
    }

   public void Attack(int damageToDeal)
    {
        throw new System.NotImplementedException();
    }
}

public enum EnemyStates
{
    WALK,
    IDLE,
    SCREAM,
    ATTACK
}
