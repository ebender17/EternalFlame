using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMeleeAttackStateData", menuName = "Entity Data/State/Melee Attack State Data")]
public class EntityMeleeAttackStateSO : ScriptableObject
{
    public float attackRadius = 0.5f;
    public int attackDamage = 1;
    public float attackCoolDown = 1f;

    public float knockDuration = 0.2f;
    public float knockThrust = 3.0f;

}
