using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityState 
{
    protected EntityStateMachine stateMachine;
    protected Entity entity;

    protected float startTime;

    protected string animBoolName;

    public EntityState(Entity entity, EntityStateMachine stateMachine, string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.entity = entity;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        entity.animator.SetBool(animBoolName, true);
    }

    public virtual void Exit()
    {
        entity.animator.SetBool(animBoolName, false);
    }

    public virtual void Execute()
    {

    }

    public virtual void ExecutePhysics()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {

    }
}
