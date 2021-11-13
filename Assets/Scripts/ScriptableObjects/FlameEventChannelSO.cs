using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Flame Event Channel")]
public class FlameEventChannelSO : EventChannelBaseSO
{
    public UnityAction<FlameDataSO> OnEventRaised;

    public void RaiseEvent(FlameDataSO value)
    {
        OnEventRaised?.Invoke(value);
    }
}
