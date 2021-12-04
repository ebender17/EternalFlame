using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemy_Dead : EntityDeadState
{
    private ChaseEnemy enemy;
    public ChaseEnemy_Dead(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityDeadStateSO stateData, ChaseEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
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
