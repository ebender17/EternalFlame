using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Entity Data/Entity/Entity Data")]
public class EntitySO : ScriptableObject
{
    public int health = 3; //number of hits before killed
    public int speed = 3;
    public float knockBackHopSpeed = 10f;

    [Header("Attack Variables")]
    public float minAgroDistanceRadius = 3.0f;
    public float closeRangeActionDistance = 1.0f;

    [Header("Stun variables")]
    public int stunResistance = 2; //indicates number of hits before stun
    public float stunRecoveryTime = 2f;
}
