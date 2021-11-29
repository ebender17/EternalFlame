using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBoolEventChannel", menuName = "Events/Bool Event Channel")]
public class BoolEventChannelSO : ScriptableObject
{
    public BoolEventAction OnEventRaised;

    public void RaiseEvent(bool gameResult)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(gameResult);
        }
    }
}
public delegate void BoolEventAction(bool gameResult);