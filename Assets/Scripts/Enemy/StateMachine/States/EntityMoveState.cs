using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMoveState : EntityState
{
    protected EntityMoveStateSO moveData;

    protected bool isPlayerInMinAgroRange;

    public EntityMoveState(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityMoveStateSO stateData) : base(entity, stateMachine, animBoolName)
    {
        moveData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();


        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgro();
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
