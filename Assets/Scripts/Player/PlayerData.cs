using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newPlayerData", menuName = "Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Player's Flame")]
    //public Light flameLight;
    public float minIntensityRange = 0f;
    public float maxIntensityRange = 1f;
    public float flickerSpeed = 0.5f;
    public float minFlameRange = 2f;
    public float maxFlameRange = 10f;

    public Material diffuseMaterial;
    public Material defaultMaterial;

    [Header("Move State")]
    public float speed = 3.0f;

    [Header("Attack State")]
    public float coolDown = 0.5f;

    [HideInInspector] public int maxHealth = 12; //This value should be 4x the amount of hearts we have
    [HideInInspector] public int currentHealth;

}
