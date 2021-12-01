using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    public int playerDamage = 25;
    public int enemyDamage = 1; 

    private Enemy enemy;
    private PlayerController player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody2D hit = collision.GetComponent<Rigidbody2D>();
            if(hit != null)
            {
                //Enemy hit
                if (collision.gameObject.CompareTag("Enemy"))
                {
                    enemy = collision.GetComponent<Enemy>();

                    if (enemy != null)
                    {
                        enemy.ChangeState(EnemyState.knockBack);
                        enemy.TakeDamage(playerDamage);
                        enemy.Knock(hit, knockTime);
                    }
                }
                else if(collision.gameObject.CompareTag("Player"))
                {
                    player = collision.GetComponent<PlayerController>();

                    if(player != null)
                    {
                        Debug.Log("Player detected in knock back.");
                        player.Knock(hit, knockTime);
                        player.TakeDamage(enemyDamage);
                    }
                }

                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);



            }
        }
    }

    
}
