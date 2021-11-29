using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolChaseEnemy : Entity
{
    public PatrolChase_Move moveState { get; private set; }
    public PatrolChase_Idle idleState { get; private set; }

    [SerializeField] private EntityIdleStateSO _idleStateData;
    [SerializeField] private EntityMoveStateSO _moveStateIdle;

    #region Move Variables
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public Transform moveSpot;
    #endregion

    public override void Start()
    {
        base.Start();

        idleState = new PatrolChase_Idle(this, stateMachine, "Idle", _idleStateData, this);
        moveState = new PatrolChase_Move(this, stateMachine, "Move", _moveStateIdle, this);

        stateMachine.Initialize(idleState);
    }
        
}
