using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;
    public Animator animator;

    public EnemyHP enemyHP;




    public EnemyState currentState;
    public EnemyDeathState deathState;
    public EnemyIdleState idleState;
    public BossState bossState;

    public playerHP playerHP;

    public BossData bossData;

    private void Awake()
    {
        player = GameObject.Find("PlaYer").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        enemyHP = GetComponent<EnemyHP>();
        playerHP = GameObject.Find("PlaYer").GetComponent<playerHP>();
        bossData = GameObject.Find("Boss").GetComponent<BossData>();

    }

    // Update is called once per frame
    void Update()
    {
        RunStateMachine();
        if (enemyHP.currentHP <= 0)
        {
            SwitchToNextState(deathState);
        }

        if (playerHP.currentplayerHP <= 0)
        {
            SwitchToNextState(idleState);
        }

        if (bossData.isUsingSpell)
        {
            SwitchToNextState(bossState);
        }
    }

    private void RunStateMachine()
    {
        EnemyState nextState = currentState?.RunCurrentState();

        if (nextState != null)
        {
            //Switch to next State
            SwitchToNextState(nextState);
        }
    }

    private void SwitchToNextState(EnemyState nextState)
    {
        currentState = nextState;
    }
}
