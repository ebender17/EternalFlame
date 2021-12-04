using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityState 
{
    protected EntityStateMachine stateMachine;
    protected Entity entity;

    protected float startTime;

    protected string animBoolName;
    protected bool isAnimationFinished;

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
        DoChecks();
    }

    public virtual void ExecutePhysics()
    {
        //DoChecks();
    }

    public virtual void DoChecks()
    {

    }

    public virtual void AnimationFinish()
    {
        Debug.Log("Inside Animation Finsihed in Entity State");
        isAnimationFinished = true;
    }
}
