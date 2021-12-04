using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackPlayer : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    private Entity enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.activeSelf)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Rigidbody2D hit = collision.GetComponent<Rigidbody2D>();
                if (hit != null)
                {
                    enemy = collision.GetComponent<Entity>();

                    if (enemy != null)
                    {
                        enemy.TakeDamage(playerData.attackHitDamage);
                        enemy.Knock(hit, playerData.knockBackDuration);

                        Vector2 difference = hit.transform.position - transform.position;
                        difference = difference.normalized * playerData.knockBackThrust;
                        hit.AddForce(difference, ForceMode2D.Impulse);
                    }


                }
            }

        }
        
    }
    
}
