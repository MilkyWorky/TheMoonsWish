using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyState
{
    public EnemyStateMachine esm;

    public override EnemyState RunCurrentState()
    {
        
        //Debug.Log("IS DEAD");
        esm.animator.Play("Death");
        return this;
    }
}
