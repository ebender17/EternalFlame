using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public EntityStateMachine stateMachine;
    
    public EntitySO entityData;

    public Rigidbody2D rb { get; private set; }
    public Animator animator { get; private set; }
    public GameObject aliveGO { get; private set; }

    //[SerializeField] private Transform _wallCheck;
    //[SerializeField] private Transform _ledgeCheck;

    [SerializeField] private Transform _playerCheck;
    //[SerializeField] private Transform _groundCheck;

    public Vector2 facingDirection { get; private set; }
    private Vector2 tempVelocity;

    private int currentHealth;
    private float currentStunResistance;
    private float lastDamageTime;
    protected bool isStunned; //flag used to transition in enemy specific stun state
    protected bool isDead; //flag used to transition in enemy specific dead state
    public int lastDamageDirection { get; private set; }

    public virtual void Start()
    {
        facingDirection = Vector2.zero;
        currentHealth = entityData.health;
        currentStunResistance = entityData.stunResistance;

        aliveGO = transform.Find("Alive").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        animator = aliveGO.GetComponent<Animator>();

        stateMachine = new EntityStateMachine();
    }

    public virtual void Update()
    {
        stateMachine.currentState.Execute();

        //animator.SetFloat("moveX", rb.velocity.x);
        //animator.SetFloat("moveY", rb.velocity.y);

        if (Time.time >= lastDamageTime + entityData.stunRecoveryTime && isStunned)
            ResetStunResistance();
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
        return Physics2D.OverlapCircle(_playerCheck.position, entityData.minAgroDistanceRadius, entityData.whatIsPlayer);
    }

    public virtual void Flip()
    {
        facingDirection *= -1;
        aliveGO.transform.Rotate(0f, 180f, 0f);
    }

    public virtual void DamageHop(float velocity)
    {
        tempVelocity.Set(rb.velocity.x, velocity);
        rb.velocity = tempVelocity;
    }

    public virtual void ResetStunResistance()
    {
        isStunned = false;
        currentStunResistance = entityData.stunResistance;
    }

    public virtual void TakeDamage(float playerXPox, int damage)
    {
        lastDamageTime = Time.time;
        currentStunResistance--;
        currentHealth -= damage;


        if (playerXPox > aliveGO.transform.position.x)
        {
            lastDamageDirection = -1;
        }
        else
        {
            lastDamageDirection = 1;
        }

        if (currentStunResistance <= 0)
            isStunned = true;

        if (currentHealth <= 0)
            isDead = true;
    }
}
