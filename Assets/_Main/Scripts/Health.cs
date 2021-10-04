using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Animator animator;
    [SerializeField] private UnityEvent onTakeDamage;
    
    private int currentHealth = default;
    public bool IsDead
    {
        get { return currentHealth <= 0;}
    }
    public int CurrentHealth 
    {
        get { return currentHealth;}
    }

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (!IsDead)
            onTakeDamage?.Invoke();

        animator.SetBool("isDead", IsDead);
    }
}
