using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newInteraction", menuName = "UI/Interaction")]
public class InteractionSO : ScriptableObject
{
    [Tooltip("The Interaction Name")]
    [SerializeField] private string interactionName = default;

    [Tooltip("The Interaction Type")]
    [SerializeField] private InteractionType interactionType = default;

    public string InteractionName => interactionName;
    public InteractionType InteractionType => interactionType;

}
