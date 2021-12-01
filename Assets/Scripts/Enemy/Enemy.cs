using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState { idle, walk, attack, knockBack }

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    public string enemyName;
    [HideInInspector]
    public EnemyState currentState;

    public int maxHits = 3;
    private int currrentHealth;

    public int baseAttack;
    private float lastDamageTime = 0f;
    public float timeBeforeNextDamage = 1f;

    private bool attackAnimationFinished;
    protected int attackCoolDown;


    public int moveSpeed;

    [HideInInspector]
    public Rigidbody2D rb;
    protected Animator animator;

    // Start is called before the first frame update
    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currrentHealth = maxHits;
        Debug.Log("Current health at start: " + currrentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }

    public void TakeDamage(int damage)
    {
        //So player cannot damage enemy more than once depending on timeBeforeNextDamageValue
        if(Time.time > lastDamageTime + timeBeforeNextDamage)
        {
            Debug.Log("Current Health before damage: " + currrentHealth);
            currrentHealth = currrentHealth - damage;
            lastDamageTime = Time.time;

            Debug.Log("Enemy Health: " + currrentHealth);
        }

        if(currrentHealth <= 0)
        {
            //TODO: Death particles
            gameObject.SetActive(false);
        }
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
            ChangeState(EnemyState.idle);
        }
    }

    void AnimationFinishedCallback(AnimationEvent evt)
    {
        if(evt.animatorClipInfo.weight > 0.5)
        {
            attackAnimationFinished = true;
            ChangeState(EnemyState.idle);
            animator.SetBool("Attack", false);
            animator.SetBool("Move", false);
            animator.SetBool("Idle", true);
        }
    }
}
