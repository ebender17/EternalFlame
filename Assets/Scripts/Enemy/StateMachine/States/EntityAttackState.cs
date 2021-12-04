using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAttackState : EntityState
{
    protected bool isPlayerInMinAgroRange;

    public EntityAttackState(Entity entity, EntityStateMachine stateMachine, string animBoolName) : base(entity, stateMachine, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgro();
    }

    public override void Enter()
    {
        base.Enter();

        isAnimationFinished = false;
        entity.SetVelocityX(0f);
        entity.SetVelocityY(0f);
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

    //Called after attack animation finishes
    //This is how we know to transition to another state
    public virtual void FinishAttack()
    {
        isAnimationFinished = true;
    }
}
