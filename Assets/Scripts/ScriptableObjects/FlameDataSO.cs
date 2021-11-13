using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FlameType { Orange = 0, Pink, Blue } 

[CreateAssetMenu(menuName = "Data/Flames")]
public class FlameDataSO : ScriptableObject
{
    [Range(0f, 100f)]
    [SerializeField] private float flameHealth = 50f;

    [SerializeField] private FlameType flameType;

    public float FlameHealth { get => flameHealth; set => flameHealth = value; }

    public FlameType FlameType { get => flameType; set => flameType = value; }
}
