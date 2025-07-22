using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kineractive
{
    [AddComponentMenu("KINERACTIVE/Utility/Text Changer")]
    public class TextChanger : MonoBehaviour
    {
        [SerializeField] InputHandler inputHandler = null;
        [SerializeField] string textString = "text" ;

        public void SetText()
        {
            inputHandler.SetUsageInstructions(textString);
        }

    }
}