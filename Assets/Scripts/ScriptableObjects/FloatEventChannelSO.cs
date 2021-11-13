using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Float Event Channel")]
public class FloatEventChannelSO : EventChannelBaseSO
{
    public UnityAction<float> OnEventRaised;

    public void RaiseEvent(float value)
    {
       OnEventRaised?.Invoke(value);
    }
}
