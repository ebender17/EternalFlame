using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerStateBase
{
    public PlayerMoveState(PlayerController player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
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

        player.animator.SetFloat("Horizontal", xInput);
        player.animator.SetFloat("Vertical", yInput);

        /*if (rawMovement.x == 1 || rawMovement.x == -1 || rawMovement.y == 1 || rawMovement.y == -1)
        {
            animator.SetFloat("LastHorizontal", rawMovement.x);
            animator.SetFloat("LastVertical", rawMovement.y);
        }*/

        if(xInput == 1 || xInput == -1 || yInput == 1 || yInput == -1)
        {
            player.animator.SetFloat("LastHorizontal", xInput);
            player.animator.SetFloat("LastVertical", yInput);
        }

        if (!isExitingState)
        {
            if(xInput == 0 && yInput == 0)
            {
                stateMachine.ChangeState(player.idleState);
            }
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
