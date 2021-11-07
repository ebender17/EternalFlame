using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;

public class TextTrackMixer : PlayableBehaviour
{
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        TextMeshProUGUI text = playerData as TextMeshProUGUI;
        string currentText = "";
        float currentAlpha = 0;

        if(!text) { return; }

        int inputCount = playable.GetInputCount(); //Count all the clips on the track.
        for(int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);

            if(inputWeight > 0f) //Check if input weight is above 0 so we know we are working with our active clip.
            {
                ScriptPlayable<TextBehavior> inputPlayable = (ScriptPlayable<TextBehavior>)playable.GetInput(i); //Use as active clip to set both the text and alpha.

                TextBehavior input = inputPlayable.GetBehaviour();
                currentText = input.text;
                currentAlpha = inputWeight;
            }
        }

        text.text = currentText;
        text.color = new Color(1, 1, 1, currentAlpha); //Set alpha color to weight of clip. Allows us to fade text in and out using ease settings on the clip.
    }
}
