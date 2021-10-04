using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamageToPlayer : MonoBehaviour
{
    [SerializeField] private int damage = 10;

    private Health playerHealth;

    private void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    public void ApplyDamage()
    {
        playerHealth.TakeDamage(damage);
    }
}
