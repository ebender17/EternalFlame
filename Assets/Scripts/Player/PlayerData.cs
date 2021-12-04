using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newPlayerData", menuName = "Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    //Health
    [HideInInspector] public int maxHealth = 12; //This value should be 4x the amount of hearts we have
    [HideInInspector] public int currentHealth;
    public float timeInvincible = 2.0f;
    
    [Header("Player's Flame")]
    //public Light flameLight;
    public float minIntensityRange = 0f;
    public float maxIntensityRange = 1f;
    public float flickerSpeed = 0.5f;
    public float minFlameRange = 2f;
    public float maxFlameRange = 10f;
    public float maxFlameStamina = 100f;
    [HideInInspector] public float currentFlameStamina;
    [HideInInspector] public float currentFlameRange;

    public Material diffuseMaterial;
    public Material defaultMaterial;

    [Header("Move State")]
    public float speed = 3.0f;

    [Header("Attack State")]
    public float coolDown = 0.5f;
    public LayerMask whatIsDamagable;
    public int attackHitDamage = 1; //Enemy health is measured by number of hits

    [Header("Knockback for enemies")]
    public float knockBackDuration = 0.2f;
    public float knockBackThrust = 3f;


}
