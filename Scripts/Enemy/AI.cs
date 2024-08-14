using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask groundLayer, playerLayer;

    private Animator animator;

    //Patrol
    public Vector3 walkPoint;
    [SerializeField]
    bool walkPointSet = false;
    public float walkPointRange;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float waitTiming;
    [SerializeField]
    private float waitBeforeMove;

    //Attack
    public float timeBetweenAttack;
    bool alreadyAttacked;

    //States
    public float sightRange, attackrange;
    public bool playerInsightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        SearchWalkPoint();
    }

    private void Update()
    {
        //Check aggro range
        playerInsightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackrange, playerLayer);
        agent.speed = moveSpeed;

        if (!playerInsightRange && !playerInAttackRange)
        {
            Patrolling();
        }

        if (playerInsightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }

        if (playerInsightRange && playerInAttackRange)
        {
            AttackPlayer();
        }
    }

    private void Patrolling()
    {
        if (!walkPointSet && waitBeforeMove <= 0)
        {
            SearchWalkPoint();
            waitBeforeMove = waitTiming;

        }

        if(walkPointSet)
        {
            agent.SetDestination(walkPoint);
            animator.SetBool("isMoving", true);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
            animator.SetBool("isMoving", false);
            animator.SetBool("isAttacking", false);
            waitBeforeMove -= Time.deltaTime;
        }
    }

    private void SearchWalkPoint()
    {
        float randomRangeZ= Random.Range(-walkPointRange, walkPointRange);
        float randomRangeX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomRangeX, transform.position.y, transform.position.z + randomRangeZ);

        //if(Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer))
        //{
        //    walkPointSet = true;
        //}
        walkPointSet = true;
    }

    private void restTime()
    {

    }

    private void ChasePlayer()
    {
        agent.isStopped = true;
        agent.isStopped = false;
        agent.SetDestination(player.position);
        animator.SetBool("isMoving", true);
        animator.SetBool("isAttacking", false);

    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            animator.SetBool("isMoving", false);
            animator.SetBool("isAttacking", true );
            Invoke(nameof(ResetAttack), timeBetweenAttack);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackrange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
