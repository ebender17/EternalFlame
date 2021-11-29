using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

[CreateAssetMenu(fileName = "PlayableDirectorEventChannel", menuName = "Events/Playable Director Event Channel")]
public class PlayableDirectorChannelSO : ScriptableObject
{
    public UnityAction<PlayableDirector, bool> OnEventRaised;

    public void RaiseEvent(PlayableDirector playable, bool isEndingCutscene)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(playable, isEndingCutscene);
    }
}
