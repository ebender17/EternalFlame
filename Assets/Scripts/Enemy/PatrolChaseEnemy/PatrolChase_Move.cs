using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolChase_Move : EntityMoveState
{

    private PatrolChaseEnemy enemy;


    public PatrolChase_Move(Entity entity, EntityStateMachine stateMachine, string animBoolName, EntityMoveStateSO stateData, PatrolChaseEnemy enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Inside Patrol Chase move state.");

        enemy.moveSpot.position = new Vector2(Random.Range(enemy.minX, enemy.maxX), Random.Range(enemy.minY, enemy.maxY));

    }

    public override void Execute()
    {
        base.Execute();
        Debug.Log("Move spot" + enemy.moveSpot.position);

        Vector2 direction = enemy.moveSpot.position - entity.transform.position;
        Debug.Log("Direction before normalization " + direction);
        direction.Normalize();
        entity.animator.SetFloat("moveX", direction.x);
        entity.animator.SetFloat("moveY", direction.y);

        entity.transform.position = Vector2.MoveTowards(entity.transform.position, enemy.moveSpot.position, moveData.speed * Time.deltaTime);

        if(Vector2.Distance(entity.transform.position, enemy.moveSpot.position) < 0.2f)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
        else if(isPlayerInMinAgroRange)
        {
            //TODO: Change to detected state
        }

        //if (Vector2.Distance(entity.gameObject.transform.position, moveSpot.position, moveData.speed * Time.deltaTime);
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
