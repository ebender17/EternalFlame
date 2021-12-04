using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStunState : EntityState
{
    protected EntityStunStateSO stunData;

    protected bool isStunTimeOver;
    protected bool isMovememtStopped;
    protected bool performCloseRangeAction;
    protected bool isPlayerInMinAgroRange;
    public EntityStunState(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityStunStateSO stateData) : base(entity, stateMachine, animBoolName)
    {
        stunData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgro();
    }

    public override void Enter()
    {
        base.Enter();

        isStunTimeOver = false;

        //TODO:
        //entity.SetVelocityAndAngle(stunData.stunKnockbackSpeed, stunData.stunKnockbackAngle);
    }

    public override void Execute()
    {
        base.Execute();

        if (Time.time >= startTime + stunData.stunTime)
            isStunTimeOver = true;
    }

    public override void ExecutePhysics()
    {
        base.ExecutePhysics();
    }

    public override void Exit()
    {
        base.Exit();

        entity.ResetStunResistance();
    }
}
