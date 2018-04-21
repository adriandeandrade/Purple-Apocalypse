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
        GetInput();
        base.Update();
    }

    void GetInput()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        direction = new Vector2(moveHorizontal, moveVertical).normalized;
    }

    //IEnumerator AttackStart()
    //{
    //    if (isAttacking && !animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
    //    {
    //        animator.SetTrigger("attack");
    //    }
    //    yield return new WaitForSeconds(0.7f);
    //    isAttacking = false;
    //    attackTrigger.enabled = false;
    //    Debug.Log("No longer attacking");
    //}
}
