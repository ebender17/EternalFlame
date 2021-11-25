using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Entity
{
    [SerializeField] private Transform target;
    [SerializeField] private float chaseRadius;
    [SerializeField] private float attackRadius;
    private Transform homePosition;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;
        //target = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        homePosition = transform;
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
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.knockBack)
            {
                //Bc rb is dynamic we do not want to be setting the transform.
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                rb.MovePosition(temp);
                ChangeState(EnemyState.walk);
            }
            
        }
    }

    private void ChangeState(EnemyState newState)
    {
        if(currentState != newState)
        {
            currentState = newState;
        }
    }
}
