using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityIdleState : EntityState
{
    protected EntityIdleStateSO idleData;

    //TODO: Have enemies only idle for a period of time and then move again (use the documented out code here). Leaving at idle for now so I can get stuff done.
    //protected bool isIdleTimeOver;

    //protected float idleTime;

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
        //isIdleTimeOver = false;
        //SetRandomIdleTime();

    }

    public override void Execute()
    {
        base.Execute();

        /*if(Time.time >= startTime + idleTime)
        {
            isIdleTimeOver = true;
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

    /*private void SetRandomIdleTime()
    {
        idleTime = Random.Range(idleData.minIdleTime, idleData.maxIdleTime);
    }*/
}
