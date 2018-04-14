using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    private PlayerController controller;

    private void Start()
    {
        controller = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            Debug.Log("enemy hit");
            //other.GetComponent<Enemy>().TakeDamage(controller.damage);
        }
    }

}
