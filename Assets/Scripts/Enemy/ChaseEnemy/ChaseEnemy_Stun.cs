using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemy_Stun : EntityStunState
{
    private ChaseEnemy enemy;
    public ChaseEnemy_Stun(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityStunStateSO stateData, ChaseEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entering stun state in chase enemy.");
    }

    public override void Execute()
    {
        base.Execute();

        if (isStunTimeOver)
        {
            if (performCloseRangeAction)
            {
                stateMachine.ChangeState(enemy.meleeAttackState);

            }    
            else if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(enemy.chaseState);
            }   
            else
            {
                stateMachine.ChangeState(enemy.idleState);
            }

        }
    }

    public override void ExecutePhysics()
    {
        base.ExecutePhysics();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
