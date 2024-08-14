using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyState
{

    public LayerMask playerLayer;

    public float attackrange;
    public bool playerInAttackRange;


    private float timer = 0f;
    public float maxDistance = 5f;
    public float maxTime = 1f;
    public EnemyStateMachine esm;
    [SerializeField] GameObject player;

    public EnemyAttackState attackState;
    public bool isInAttackRange;


    private void Start()
    {
        player = GameObject.Find("PlaYer");
    }
    public override EnemyState RunCurrentState()
    {
        if (playerInAttackRange)
        {
            isInAttackRange = true;
        }

        if (isInAttackRange)
        {
            isInAttackRange = false;
            playerInAttackRange = false;
            return attackState;
        }
        else
        {
            ChasePlayer();
            return this;
        }
    }

    private void ChasePlayer()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackrange, playerLayer);
        if (!esm.agent.enabled) return;

        timer -= Time.deltaTime;

        if (!esm.agent.hasPath)
        {
            esm.agent.destination = player.transform.position;
        }

        if (timer < 0)
        {
            Vector3 direction = player.transform.position - esm.agent.destination;
            direction.y = 0;

            float sqrDistance = direction.sqrMagnitude;
            if (sqrDistance > maxDistance * maxDistance)
            {
                if (esm.agent.pathStatus != UnityEngine.AI.NavMeshPathStatus.PathPartial)
                {
                    esm.agent.destination = player.transform.position;
                }

            }
            timer = maxTime;
        }
        esm.animator.SetBool("isMoving", true);
        esm.agent.destination = player.transform.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackrange);
    }
}
