using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateBase : PlayerState
{
    protected float xInput;
    protected float yInput;
    private bool _attackInput;
    public PlayerStateBase(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
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

        //Get inputs 
        xInput = player.normInputX;
        yInput = player.normInputY;
        _attackInput = player.attackInput;

        //TODO: Transition to attack State
        if(_attackInput)
        {
            stateMachine.ChangeState(player.attackState);
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
}
