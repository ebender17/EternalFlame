using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        currentState = EnemyState.idle;
        animator.SetBool("Idle", true);
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    private void CheckDistance()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= chaseRadius && distance > attackRadius)
        {
            if(currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.knockBack)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                Vector3 newDistance = (temp - transform.position).normalized;
                
                rb.MovePosition(temp);
                ChangeState(EnemyState.walk);
                animator.SetBool("Idle", false);
                animator.SetBool("Move", true);

                animator.SetFloat("moveX", newDistance.x);
                animator.SetFloat("moveY", newDistance.y);

                if (newDistance.x == 1 || newDistance.x == -1 || newDistance.x == 1 || newDistance.x == -1)
                {
                    animator.SetFloat("lastHorizontal", newDistance.x);
                    animator.SetFloat("lastVertical", newDistance.y);
                }
            }
            
        }
        else if(distance <= attackRadius)
        {
            ChangeState(EnemyState.attack);
            animator.SetBool("Attack", true);
            animator.SetBool("Idle", false);
            animator.SetBool("Move", false);

        }
    }

    
}
