using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemy_MeleeAttack : EntityMeleeAttackState
{
    private ChaseEnemy enemy;
    private float lastAttack;
    public ChaseEnemy_MeleeAttack(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityMeleeAttackStateSO stateData, ChaseEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        lastAttack = Time.time;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Execute()
    {
        base.Execute();

        if(isAnimationFinished)
        {
            if(isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(enemy.playerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(enemy.idleState);
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

    public bool CheckIfCanAttack()
    {
        return Time.time >= lastAttack + attackData.attackCoolDown;
    }
}
