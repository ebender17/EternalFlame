using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk, 
    attack, 
    knockBack
}
public class Entity : MonoBehaviour
{
    public EntityStateMachine stateMachine;
    public EntitySO entityData;

    public Rigidbody2D rb { get; private set; }
    public Animator animator { get; private set; }
    public GameObject aliveGO { get; private set; }

    [SerializeField] private Transform _playerCheck;

    private Vector2 tempVelocity;

    private int currentHealth;
    private float lastDamageTime;
    private float currentStunResistance;
    protected bool isStunned;
    protected bool isDead;

    //TODO: Delete
    public EnemyState currentState;
    public int health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;

    // Start is called before the first frame update
    public virtual void Start()
    {
        currentHealth = entityData.health;
        currentStunResistance = entityData.stunResistance;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        stateMachine = new EntityStateMachine();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        //stateMachine.currentState.Execute();
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.ExecutePhysics();
    }

    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.OverlapCircle(_playerCheck.position, entityData.minAgroDistance, entityData.whatIsPlayer);
    }

}
