using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToStateMachine : MonoBehaviour
{
    //Set in Start of Entity
    public EntityAttackState attackState;
    public void FinishedAttack()
    {
        Debug.Log("Inside atsm");
        attackState.FinishAttack();
    }

    void AnimationFinishedCallback(AnimationEvent evt)
    {
        if (evt.animatorClipInfo.weight > 0.5 || evt.animatorClipInfo.weight < -0.5)
        {
            Debug.Log("Inside atsm");
            attackState.FinishAttack();
        }
    }
}
