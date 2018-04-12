using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : LivingEntity
{
    [SerializeField] private int health = 100;
    [SerializeField] private Text hpText;
    private float waitTime = 1f;
    private Transform target;
    private bool isAttacking = false;

    private void Start()
    {
        currentHealth = 100;
        target = FindObjectOfType<PlayerController>().transform;
        StartCoroutine(LocatePlayer());
    }

    IEnumerator LocatePlayer()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        float distanceFromPlayer = Vector2.Distance(target.position, transform.position);
        Debug.Log(distanceFromPlayer);
        if (distanceFromPlayer > AttackDistance)
        {
            Movement(MoveSpeed, direction);
            yield return new WaitForSeconds(waitTime);
        }
        yield return new WaitForSeconds(waitTime);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    private void GetTargetInformation()
    {

    }

    private void Update()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        Movement(MoveSpeed, direction);
    }
}
