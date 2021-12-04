using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolChase_Idle : EntityIdleState
{
    PatrolChaseEnemy enemy;
    public PatrolChase_Idle(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityIdleStateSO stateData, PatrolChaseEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        Debug.Log("Inside patrol chase idle state.");
    }

    public override void Execute()
    {
        base.Execute();

        if(isPlayerInMinAgroRange)
        {
            //TODO: Change to attack state
        }
        /*else if(isIdleTimeOver)
        {
            stateMachine.ChangeState(enemy.moveState);
        }*/
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
