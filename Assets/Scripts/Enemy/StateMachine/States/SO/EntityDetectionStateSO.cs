using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newDetectionStateData", menuName = "Entity Data/State/Detection State Data")]
public class EntityDetectionStateSO : ScriptableObject
{
    public float detectionWaitTime = 0.01f;
}
