using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerController player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected float startTime;

    protected bool isAnimationFinished;
    protected bool isExitingState;

    private string _animName;

    public PlayerState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this._animName = animName;
    }

    public virtual void Enter()
    {
        player.animator.SetBool(_animName, true);
        startTime = Time.time;
        isAnimationFinished = false;
        isExitingState = false;
    }

    public virtual void Exit()
    {
        player.animator.SetBool(_animName, false);
        isExitingState = true;
    }

    public virtual void Execute()
    {

    }

    public virtual void ExecutePhysics()
    {
        DoChecks();
    }

    public virtual void DoChecks() { }

    public virtual void AnimationFinish() => isAnimationFinished = true;
}
