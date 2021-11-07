using UnityEngine;
using UnityEngine.Playables;

public class TextClip : PlayableAsset
{
    public string text;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<TextBehavior>.Create(graph);

        TextBehavior textBehavior = playable.GetBehaviour();

        textBehavior.text = this.text;

        return playable;
    }
}
