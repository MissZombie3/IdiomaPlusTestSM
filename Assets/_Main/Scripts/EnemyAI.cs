using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Patrol,
    Chase,
    Attack
}

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private Animator animator;
    [SerializeField] private float detectionRange;
    [SerializeField] private float stopChaseRange;
    [SerializeField] private float attackRange;
    [SerializeField] private float stopAttackRange;
    [SerializeField] private float damping = 10;

    private NavMeshAgent agent;
    private int currentPoint;
    private EnemyState currentState = EnemyState.Patrol;
    private Transform playerTransform;
    private Health playerHealth;
    private Health myHealth;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = playerTransform.GetComponent<Health>();
        myHealth = GetComponent<Health>();
    }

    private void Update()
    {
        if (!playerHealth.IsDead && !myHealth.IsDead)
        {
            SetState();
            agent.isStopped = currentState == EnemyState.Attack;
        }
        else
        {
            agent.isStopped = false;
            currentState = EnemyState.Patrol;
            //Patrol();
        }
        animator.SetBool("isAttacking", currentState == EnemyState.Attack);
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    private void SetState()
    {
        switch (currentState)
        {
            case EnemyState.Patrol:
                Patrol();
                break;

            case EnemyState.Chase:
                Chase();
                break;

            case EnemyState.Attack:
                Attack();
                break;
        }
    }

    private void Patrol()
    {
        Transform point = patrolPoints[currentPoint];
        float distance = Vector3.Distance(transform.position, point.position);

        if (distance < 1F)
        {
            currentPoint++;

            if (currentPoint >= patrolPoints.Length)
                currentPoint = 0;
        }

        agent.SetDestination(point.position);
        if (Vector3.Distance(playerTransform.position, transform.position) <= detectionRange)
            currentState = EnemyState.Chase;
    }

    private void Chase()
    {
        agent.SetDestination(playerTransform.position);

        if (Vector3.Distance(playerTransform.position, transform.position) > stopChaseRange)
            currentState = EnemyState.Patrol;

        if (Vector3.Distance(playerTransform.position, transform.position) <= attackRange)
            currentState = EnemyState.Attack;
    }

    private void Attack()
    {
        agent.velocity = Vector3.zero;
        var lookPos = playerTransform.position - transform.position;
        lookPos.y = 0;

        if (lookPos.magnitude != 0)
        {
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
        }

        if (Vector3.Distance(playerTransform.position, transform.position) > stopAttackRange)
            currentState = EnemyState.Chase;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange); 
        
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, stopChaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, stopAttackRange);
    }
}
