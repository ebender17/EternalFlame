using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Entity Data/Entity/Entity Data")]
public class EntitySO : ScriptableObject
{
    public int health = 30;
    public int speed = 3;
    public float knockBackHopSpeed = 10f;

    [Header("Check Variables")]
    public float wallCheckDistance = 0.2f; //TODO: Replace check with pathfinding

    [Header("Attack Variables")]
    public float minAgroDistanceRadius = 3.0f;
    public float maxAgroDistanceRadius = 4.0f;
    public LayerMask whatIsPlayer;
    public float closeRangeActionDistance = 1.0f;
    public uint touchDamage = 5;

    [Header("Stun variables")]
    public int stunResistance = 3; //indicates number of hits before stun
    public float stunRecoveryTime = 2f;
}
