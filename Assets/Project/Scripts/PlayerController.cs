using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{
    [SerializeField] private Collider2D attackTrigger;

    protected override void Start()
    {
        base.Start();
        attackTrigger.enabled = false;
    }

    protected override void Update()
    {
        //if (isAttacking)
        //    return;

        GetInput();

        if (!isAttacking && !animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && !isWalking)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(Attack(0.75f));
            }
        }

        base.Update();
    }

    void GetInput()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        direction = new Vector2(moveHorizontal, moveVertical).normalized;
    }
}
