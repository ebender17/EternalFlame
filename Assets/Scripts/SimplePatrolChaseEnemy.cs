using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStates { idle, move, attack, knockBack }

public class SimplePatrolChaseEnemy : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private float startTime;
    private float waitTime;
    [SerializeField]
    private float minWaitTime;
    [SerializeField]
    private float maxWaitTime;

    public Transform[] pathPoints;
    private int pathPointIndex = 0;
    private Transform homePosition;

    private Rigidbody2D rb;
    private Vector2 desiredVelocity;
    private Vector2 currentVelocity;
    public Vector2 CurrentVelocity { get => currentVelocity; set => currentVelocity = value; }
    private Animator animator;

    private EnemyStates currentState;
    public EnemyStates CurrentState { get => currentState; set => currentState = value; }

    #region Attack
    [SerializeField]
    private float playerCheckRadius;
    [SerializeField]
    private float playerAttackRadius;
    [SerializeField]
    private LayerMask whatIsPlayer;
    #endregion

    #region Damage & Health
    [SerializeField]
    private int maxHealth = 100;
    private int currentHealth;

    [SerializeField]
    private int damage = 10;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentState = EnemyStates.idle;
        animator.SetBool("Idle", true);

        startTime = Time.time;
        waitTime = RandomRange(minWaitTime, maxWaitTime);

        CalculateNextVelocity();

        homePosition = pathPoints[0];
    }

    private float RandomRange(float min, float max)
    {
        return Random.Range(min, max);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == EnemyStates.idle)
        {
            //Go to Move State
            if(Time.time >= startTime + waitTime)
            {
                CalculateNextVelocity();
                rb.velocity = desiredVelocity;

                animator.SetFloat("moveX", rb.velocity.x);
                animator.SetFloat("moveY", rb.velocity.y);

                animator.SetBool("Idle", false);
                animator.SetBool("Move", true);

                ChangeState(EnemyStates.move);
            }
        }
        else if (currentState == EnemyStates.move)
        {
            //Switch to idle state when reached target
            if(Vector2.Distance(transform.position, pathPoints[pathPointIndex].position) < 0.1f)
            {
                rb.velocity = Vector2.zero;
                startTime = Time.time;
                waitTime = RandomRange(minWaitTime, maxWaitTime);
                SetNextPathPoint();

                ChangeState(EnemyStates.idle);
                animator.SetBool("Idle", true);
                animator.SetBool("Move", false);
            }
            
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = currentVelocity;
    }

    private void ChangeState(EnemyStates newState)
    {
        if(newState != currentState)
        {
            currentState = newState;
        }
    }

    private void SetNextPathPoint()
    {
        if(pathPointIndex < pathPoints.Length - 1)
        {
            pathPointIndex++;
        }
        else
        {
            pathPointIndex = 0;
        }
    }

    private void CalculateNextVelocity()
    {
        Debug.Log(pathPoints[pathPointIndex].position + " and " + transform.position);
        desiredVelocity = (pathPoints[pathPointIndex].position - transform.position).normalized * speed;
        Debug.Log("Path point index: " + pathPointIndex + "Velocity: " + desiredVelocity);
    }

    public virtual bool CheckPlayerInAgroRange()
    {
        return Physics2D.OverlapCircle(transform.position, playerCheckRadius, whatIsPlayer);
    }

    public void TakeDamage(int damange)
    {
        currentHealth = currentHealth - damange;

        if(currentHealth <= 0)
        {
            //TODO: Add death particles
            gameObject.SetActive(false);
        }
    }
}
