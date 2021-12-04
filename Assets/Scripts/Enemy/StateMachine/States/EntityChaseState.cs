using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityChaseState : EntityState
{
    EntityChaseStateSO chaseData;

    protected bool isPlayerInMinAgroRange;
    protected bool performCloseRangeAction;

    public EntityChaseState(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityChaseStateSO stateData ) : base(entity, stateMachine, animBoolName)
    {
        chaseData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgro();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();

        //TODO: Set velocity
    }

    public override void Execute()
    {
        base.Execute();

        //Not using rb to move enemy currently...
        //Vector2 direction;
        if (entity.target != null)
        {
            Vector2 direction = (entity.target.transform.position - entity.transform.position);
            direction = NormalizeDirection(direction);

            entity.transform.position = Vector2.MoveTowards(entity.transform.position, entity.target.transform.position, chaseData.chaseSpeed * Time.deltaTime);

            entity.animator.SetFloat("moveX", direction.x);
            entity.animator.SetFloat("moveY", direction.y);

            if (direction.x == 1 || direction.x == -1 || direction.x == 1 || direction.x == -1)
            {
                entity.animator.SetFloat("lastHorizontal", direction.x);
                entity.animator.SetFloat("lastVertical", direction.y);
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

    private Vector2 NormalizeDirection(Vector2 movement)
    {
        Vector2 normalized;

        if(Mathf.Abs(movement.x) > 0.5f)
        {
            normalized.x = (int)(movement * Vector2.right).normalized.x;
        }
        else
        {
            normalized.x = 0;
        }

        if (Mathf.Abs(movement.y) > 0.5f)
        {
            normalized.y = (int)(movement * Vector2.up).normalized.y;
        }
        else
        {
            normalized.y = 0;
        }
        return normalized;

        /*rawMovement = inputMovement;

        if (Mathf.Abs(rawMovement.x) > 0.5f)
        {
            normInputX = (int)(rawMovement * Vector2.right).normalized.x;
        }
        else
        {
            normInputX = 0;
        }

        if (Mathf.Abs(rawMovement.y) > 0.5f)
        {
            normInputY = (int)(rawMovement * Vector2.up).normalized.y;
        }
        else
        {
            normInputY = 0;
        }*/
    }
}
