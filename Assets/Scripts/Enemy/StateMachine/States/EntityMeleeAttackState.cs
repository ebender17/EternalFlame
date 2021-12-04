using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMeleeAttackState : EntityAttackState
{
    protected EntityMeleeAttackStateSO attackData;
    public EntityMeleeAttackState(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityMeleeAttackStateSO stateData) : base(entity, stateMachine, animBoolName)
    {
        attackData = stateData;
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
