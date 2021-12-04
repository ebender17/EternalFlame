using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityPlayerDetectedState : EntityState
{
    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    protected bool performLongRangeAction;
    protected bool performCloseRangeAction;

    protected EntityDetectionStateSO detectionData;
    public EntityPlayerDetectedState(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityDetectionStateSO stateData) : base(entity, stateMachine, animBoolName)
    {
        detectionData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgro();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();

    }

    public override void Enter()
    {
        base.Enter();

        entity.SetVelocityX(0);
        entity.SetVelocityY(0);
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
