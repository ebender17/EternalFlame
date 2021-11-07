using UnityEngine;
using TMPro;
using UnityEngine.Timeline;
using UnityEngine.Playables;

[TrackBindingType(typeof(TextMeshProUGUI))]
[TrackClipType(typeof(TextClip))]
public class TextTrack : TrackAsset
{
    //Tell track to use custom track mixer to control playable behaviors.
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<TextTrackMixer>.Create(graph, inputCount);
    }
}
