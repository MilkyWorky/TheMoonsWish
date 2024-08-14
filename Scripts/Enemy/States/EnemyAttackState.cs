using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyStateMachine esm;
    public EnemyChaseState enemyChase;

    public Transform player;
    public Transform attacker;

    public LayerMask playerLayer;
    public bool isNotAttacking;
    public bool playerInAttackRange;
    public float attackrange;

    private void Start()
    {
        attackrange = enemyChase.attackrange;
        player = GameObject.Find("PlaYer").transform;
        attacker = GetComponent<Transform>().parent.parent;
    }
    public override EnemyState RunCurrentState()
    {
        if (playerInAttackRange == false)
        {
            isNotAttacking = true;
        }
        else
        {
            isNotAttacking = false;
        }
        
        if (isNotAttacking)
        {
            AttackPlayer();
            //isNotAttacking= false;
            //playerInAttackRange = true;
            
            if (esm.animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.9f)
            {
                return null;
            }
            else
            {
                esm.animator.SetBool("isMoving", true);
                esm.animator.SetBool("isAttacking", false);
                return enemyChase;

            }
            
           
            
            
        }
        else
        {
            esm.animator.SetBool("isMoving", false);
            esm.animator.SetBool("isAttacking", true);
            //Debug.Log("Attacked");
            AttackPlayer();
            if (esm.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            {
                attacker.transform.LookAt(player);
            }
                
            return this;
        }

    }

    private void AttackPlayer()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackrange, playerLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackrange);
    }
}
