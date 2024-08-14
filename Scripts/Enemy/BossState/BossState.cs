using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState : EnemyState
{
    public EnemyStateMachine esm;
    public EnemyIdleState idleState;
    public BossData bossData;

    public override EnemyState RunCurrentState()
    {

        if (bossData.isUsingSpell)
        {
            return this;
        }
        else
        {
            return idleState;
        }

    }
}
