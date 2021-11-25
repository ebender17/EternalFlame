using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    public bool canAttack { get; private set; }
    private float lastAttackTime;

    public PlayerAttackState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        canAttack = false;
        lastAttackTime = Time.time;
        player.OnAttackCanceled();
    }

    public override void Execute()
    {
        base.Execute();

        if(isAnimationFinished)
        {
            isAbilityDone = true;
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

    public bool CheckIfCanAttack()
    {
        return Time.time >= lastAttackTime + playerData.coolDown;
    }


}
