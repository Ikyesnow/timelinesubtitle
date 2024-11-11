using TMPro;
using UnityEngine;
using UnityEngine.Playables;

namespace hrspecian.timelinesubtitle.runtime
{
    public class SubtitleTrackMixer : PlayableBehaviour
    {
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            TextMeshProUGUI text = playerData as TextMeshProUGUI;
            string currentText = "";
            float currentAlpha = 0f;

            if (!text) { return; }

            int inputCount = playable.GetInputCount();
            for (int i = 0; i < inputCount; i++)
            {
                float inputWeight = playable.GetInputWeight(i);

                if (inputWeight > 0f)
                {
                    ScriptPlayable<SubtitleBehaviour> inputPlayable = (ScriptPlayable<SubtitleBehaviour>)playable.GetInput(i);

                    SubtitleBehaviour input = inputPlayable.GetBehaviour();
                    currentText = input.subtitleText;
                    currentAlpha = inputWeight;
                }
            }

            text.text = currentText;

            Color color = text.color;
            color.a = currentAlpha;

            text.color = color;
        }
    }

}