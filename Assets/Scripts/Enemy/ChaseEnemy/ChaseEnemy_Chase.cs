using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemy_Chase : EntityChaseState
{
    private ChaseEnemy enemy;
    public ChaseEnemy_Chase(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityChaseStateSO stateData, ChaseEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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
    }

    public override void Execute()
    {
        base.Execute();

        if(performCloseRangeAction)
        {
            stateMachine.ChangeState(enemy.meleeAttackState);
        }
        else if(!isPlayerInMinAgroRange)
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
