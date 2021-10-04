using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRate;
    [SerializeField] private float attackRadius;
    [SerializeField] private int attackDamage;

    private Camera mainCamera;
    private float nextAttack = 0;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && Time.time > nextAttack)
        {
            Vector3 lookDirection = transform.position + mainCamera.transform.forward;
            lookDirection.y = 0;
            transform.LookAt(lookDirection);

            animator.ResetTrigger("attack");
            animator.SetTrigger("attack");
            nextAttack = Time.time + attackRate;
        }
    }

    public void ApplyDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(attackPoint.position, attackRadius);

        foreach (Collider col in colliders)
        {
            Health collHealth = null;
            if (!col.CompareTag("Player") && col.TryGetComponent(out collHealth))
                collHealth.TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
