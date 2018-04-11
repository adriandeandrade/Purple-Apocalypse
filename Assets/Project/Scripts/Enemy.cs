using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : LivingEntity
{
    [SerializeField] private int health = 100;
    [SerializeField] private int currentHealth;
    [SerializeField] private Text hpText;
    private float moveSpeed = 3f;
    private int cooldown = 1;
    private Transform player;

    private void Start()
    {
        currentHealth = 100;
        player = FindObjectOfType<PlayerController>().transform;
        InvokeRepeating("LocatePlayer", 0f, cooldown);
    }

    void LocatePlayer()
    {
        //Debug.Log("Locating player");
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        MoveEnemy(directionToPlayer);
    }

    void MoveEnemy(Vector2 dir)
    {
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    private void Update()
    {
        Invoke("LocatePlayer", 1f);
        hpText.text = "HP: " + currentHealth.ToString();
    }
}
