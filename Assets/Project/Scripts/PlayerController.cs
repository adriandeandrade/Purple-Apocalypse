using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : LivingEntity
{
    //[SerializeField] private float moveSpeed;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Collider2D attackTrigger;

    private Vector2 movement;
    private bool facingRight = true;
    private bool isAttacking = false;

    [SerializeField] private float attackCooldown = 0.3f;
    private float attackTimer = 0;
    public int damage = 10;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        attackTrigger.enabled = false;
        movement = Vector2.zero;
    }

    private void Update()
    {
        AttackInput();
        Movement();
        Flip();

    }

    private void Movement()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        movement = new Vector2(moveHorizontal, moveVertical).normalized;
        bool isWalking = (Mathf.Abs(moveHorizontal) + Mathf.Abs(moveVertical)) > 0;
        animator.SetBool("isWalking", isWalking);

        if (isWalking && !animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            transform.Translate(movement * MoveSpeed * Time.deltaTime);
        }
    }

    private void Flip()
    {
        if (isAttacking)
            return;

        if (movement.x > 0 && !facingRight || movement.x < 0 && facingRight && !isAttacking)
        {
            facingRight = !facingRight;
            Vector3 currentScale = transform.localScale;
            currentScale.x *= -1;
            transform.localScale = currentScale;
        }
    }

    private void AttackInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isAttacking = true;
            attackTrigger.enabled = true;
            StartCoroutine(AttackStart());
        }
    }

    IEnumerator AttackStart()
    {
        if (isAttacking && !animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            animator.SetTrigger("attack");
        }
        yield return new WaitForSeconds(0.7f);
        isAttacking = false;
        attackTrigger.enabled = false;
        Debug.Log("No longer attacking");
    }
}
