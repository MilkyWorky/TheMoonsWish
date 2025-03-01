using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static EnemyHP;

public class EnemyIdleState : EnemyState
{
    public EnemyStateMachine esm;
    public float range; //radius of sphere

    public LayerMask playerLayer;

    public float
        sightRange;
    public bool
        playerInsightRange;

    public Transform centrePoint;


    public EnemyChaseState chaseState;
    public bool canSeePlayer;


    public override EnemyState RunCurrentState()
    {
        if (playerInsightRange)
        {
            canSeePlayer = true;
        }
        if(canSeePlayer)
        {
            canSeePlayer = false;
            playerInsightRange = false;
            playerInsightRange = false;
            return chaseState;
        }
        else
        {
            if(esm.enemyHP.enemyType.Equals(EnemyType.Boss))
            {
                BossIdle();
                return this;
            }
            else
            {
                Patrolling();
                return this;
            }
            
        }
        
    }

    private void BossIdle()
    {
        playerInsightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        esm.animator.SetBool("isAttacking", false);
        esm.animator.SetBool("isMoving", false);
    }

    private void Patrolling()
    {
        playerInsightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        if (esm.agent.remainingDistance <= esm.agent.stoppingDistance) //done with path
        {
            esm.animator.SetBool("isMoving", true);
            esm.animator.SetBool("isAttacking", false);
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                esm.agent.SetDestination(point);
            }
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
