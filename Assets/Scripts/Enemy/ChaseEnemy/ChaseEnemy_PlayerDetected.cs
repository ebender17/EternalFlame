using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemy_PlayerDetected : EntityPlayerDetectedState
{
    private ChaseEnemy enemy;
    public ChaseEnemy_PlayerDetected(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityDetectionStateSO stateData, ChaseEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Execute()
    {
        base.Execute();

       
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

    public override void ExecutePhysics()
    {
        base.ExecutePhysics();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
