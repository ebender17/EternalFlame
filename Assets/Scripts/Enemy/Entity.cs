using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public EntityStateMachine stateMachine;
    
    public EntitySO entityData;

    public Rigidbody2D rb { get; private set; }
    public Animator animator { get; private set; }

    //[SerializeField] private Transform _playerCheck;

    private Vector2 tempVelocity;

    public Transform target;

    private int currentHealth;
    private float currentStunResistance;
    private float lastDamageTime;
    protected bool isStunned; //flag used to transition in enemy specific stun state
    protected bool isDead; //flag used to transition in enemy specific dead state

    
    public virtual void Start()
    {
        currentHealth = entityData.health;
        currentStunResistance = entityData.stunResistance;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        stateMachine = new EntityStateMachine();
    }

    public virtual void Update()
    {
        stateMachine.currentState.Execute();

        //TODO: Need to set y velocity in animator?

        if (Time.time >= lastDamageTime + entityData.stunRecoveryTime && isStunned)
        {
            ResetStunResistance();
        }
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.ExecutePhysics();
    }

    public virtual void SetVelocityX(float velocity)
    {
        tempVelocity.Set(velocity, rb.velocity.y);
        rb.velocity = tempVelocity;
    }

    public virtual void SetVelocityY(float velocity)
    {
        tempVelocity.Set(rb.velocity.x, velocity);
        rb.velocity = tempVelocity;
    }

    public virtual void SetVelocity(Vector2 velocity)
    {
        rb.velocity = velocity;
    }

    public virtual void SetVelocityAndAngle(float speed, Vector2 angle, int direction)
    {
        angle.Normalize();
        tempVelocity.Set(angle.x * speed * direction, angle.y * speed);
        rb.velocity = tempVelocity;

    }

    public virtual bool CheckPlayerInMinAgro()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        return distance <= entityData.minAgroDistanceRadius && distance > entityData.closeRangeActionDistance;
    }

    public virtual bool CheckPlayerInCloseRangeAction()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        return distance <= entityData.closeRangeActionDistance;
    }

    void AnimationFinishedCallback(AnimationEvent evt)
    {
        if (evt.animatorClipInfo.weight > 0.5)
        {
            stateMachine.currentState.AnimationFinish();
        }
    }

    public void FinishedAnimation()
    {
        stateMachine.currentState.AnimationFinish();
    }

    public virtual void ResetStunResistance()
    {
        isStunned = false;
        currentStunResistance = entityData.stunResistance;
    }

    public void Knock(Rigidbody2D rigidBody, float knockTime)
    {
        StartCoroutine(KnockBackCoroutine(rigidBody, knockTime));
    }
    private IEnumerator KnockBackCoroutine(Rigidbody2D rigidBody, float knockTime)
    {
        if (rigidBody != null)
        {
            yield return new WaitForSeconds(knockTime);
            rigidBody.velocity = Vector2.zero;
        }
    }

    public virtual void TakeDamage(int damage)
    {
        lastDamageTime = Time.time;
        currentStunResistance--;
        currentHealth -= damage;

        if (currentStunResistance <= 0)
            isStunned = true;

        if (currentHealth <= 0)
            isDead = true;
    }

#if UNITY_EDITOR
    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, entityData.closeRangeActionDistance);
        Gizmos.DrawWireSphere(transform.position, entityData.minAgroDistanceRadius);
    }

#endif
}
