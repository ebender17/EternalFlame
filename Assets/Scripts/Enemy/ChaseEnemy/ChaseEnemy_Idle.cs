using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemy_Idle : EntityIdleState
{
    private ChaseEnemy enemy;
    public ChaseEnemy_Idle(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityIdleStateSO stateData, ChaseEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if(isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
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
