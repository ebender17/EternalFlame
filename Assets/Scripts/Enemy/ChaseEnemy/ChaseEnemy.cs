using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemy : Entity
{
    public ChaseEnemy_Idle idleState { get; private set; }
    public ChaseEnemy_PlayerDetected playerDetectedState { get; private set; }
    public ChaseEnemy_Chase chaseState { get; private set; }
    public ChaseEnemy_MeleeAttack meleeAttackState { get; private set; }
    public ChaseEnemy_Stun stunState { get; private set; }
    public ChaseEnemy_Dead deadState { get; private set; }

    [SerializeField] private EntityIdleStateSO _idleStateData;
    [SerializeField] private EntityDetectionStateSO _detectionStateData;
    [SerializeField] private EntityChaseStateSO _chaseStateData;
    [SerializeField] private EntityMeleeAttackStateSO _meleeAttackStateData;
    [SerializeField] private EntityStunStateSO _stunStateData;
    [SerializeField] private EntityDeadStateSO _deadStateData;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        idleState = new ChaseEnemy_Idle(this, stateMachine, "Idle", _idleStateData, this);
        playerDetectedState = new ChaseEnemy_PlayerDetected(this, stateMachine, "PlayerDetected", _detectionStateData, this);
        chaseState = new ChaseEnemy_Chase(this, stateMachine, "Chase", _chaseStateData, this);
        meleeAttackState = new ChaseEnemy_MeleeAttack(this, stateMachine, "MeleeAttack", _meleeAttackStateData, this);
        stunState = new ChaseEnemy_Stun(this, stateMachine, "Stun", _stunStateData, this);
        deadState = new ChaseEnemy_Dead(this, stateMachine, "Dead", _deadStateData, this);

        stateMachine.Initialize(idleState);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if(isDead)
        {
            stateMachine.ChangeState(deadState);
        }
        else if(isStunned)
        {
            stateMachine.ChangeState(stunState);
        }
    }
}
