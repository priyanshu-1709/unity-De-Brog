using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kineractive
{
    [AddComponentMenu("KINERACTIVE/Utility/Text Changer")]
    public class InstructionChanger : MonoBehaviour
    {
        [SerializeField] InputHandler inputHandler = null;
        [SerializeField] string textString = "text" ;
        [SerializeField] bool changeText = false;
        [SerializeField] Texture controlsTexture = null;
        [SerializeField] bool changeControlsTexture = false;
        [SerializeField] Texture crosshairTexture = null;
        [SerializeField] bool changeCrosshairTexture = false;

        public void SetInstructions()
        {
            if(changeText)
                inputHandler.SetUsageInstructions(textString);

            if(changeControlsTexture)
                inputHandler.SetControlsIcon(controlsTexture);

            if(changeCrosshairTexture)
            inputHandler.SetCrosshairIcon(crosshairTexture);
        }

    }
}