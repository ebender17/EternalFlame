using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityIdleState : EntityState
{
    protected EntityIdleStateSO idleData;

    protected bool isIdleTimeOver;

    protected float idleTime;

    protected bool isPlayerInMinAgroRange;

    public EntityIdleState(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityIdleStateSO stateData) : base(entity, stateMachine, animBoolName)
    {
        idleData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgro();
    }

    public override void Enter()
    {
        base.Enter();

        entity.SetVelocity(Vector2.zero);
        isIdleTimeOver = false;
        SetRandomIdleTime();

    }

    public override void Execute()
    {
        base.Execute();

        if(Time.time >= startTime + idleTime)
        {
            isIdleTimeOver = true;
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

    private void SetRandomIdleTime()
    {
        idleTime = Random.Range(idleData.minIdleTime, idleData.maxIdleTime);
    }
}
