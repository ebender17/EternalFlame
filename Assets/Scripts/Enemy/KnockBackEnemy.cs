using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackEnemy : MonoBehaviour
{
    [SerializeField] EntityMeleeAttackStateSO attackData;

    private PlayerController player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D hit = collision.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                player = collision.GetComponent<PlayerController>();
                if(player != null)
                {
                    if(!player.isInvincible && player.gameObject.activeSelf)
                    {
                        player.Knock(hit, attackData.knockDuration);
                        player.TakeDamage(attackData.attackDamage);


                        Vector2 difference = hit.transform.position - transform.position;
                        difference = difference.normalized * attackData.knockThrust;
                        hit.AddForce(difference, ForceMode2D.Impulse);
                    } 
                }
            }
        }
    }
}
