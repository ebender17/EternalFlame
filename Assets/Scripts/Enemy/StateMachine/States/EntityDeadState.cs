using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDeadState : EntityState
{
    protected EntityDeadStateSO deadData;
    public EntityDeadState(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityDeadStateSO stateData) : base(entity, stateMachine, animBoolName)
    {
        deadData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        //TODO: Death particles

        entity.gameObject.SetActive(false);
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
}
