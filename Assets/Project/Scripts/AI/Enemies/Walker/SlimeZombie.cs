using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeZombie : Enemy
{
    private bool isScreaming;
    [SerializeField] private float screamCooldown;

    protected override void Start()
    {
        base.Start();
        isScreaming = false;
        screamCooldown = Random.Range(5f, 25f);
    }

    protected override void Update()
    {
        base.Update();
        screamCooldown -= Time.deltaTime;
        if(screamCooldown <= 0)
        {
            Scream();
            isScreaming = true;
            screamCooldown = Random.Range(5f, 25f);
        }
    }

    private void Scream()
    {
        animator.SetBool("isWalking", false);
        animator.SetTrigger("scream");
    }
}
