using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// When the Interactive Checker activates this script, this script moves the correct hand into a ready to interact position,
/// changes the cursor to the one on this script, and makes the usage instructions appear.
/// Hand that is not used is returned to assigned resting position.
/// hands are returned on disable
/// </summary>

namespace Kineractive
{
    [AddComponentMenu("KINERACTIVE/Handler/Input Handler Resting")]
    public class InputHandlerResting : InputHandler
    {
        protected void Start()
        {
            crosshairScale = KineractiveManager.Instance.DefaultCrosshairScale;
        }

        protected override void OnEnable()
        {
            foreach (KineractiveInput kineractiveInput in KineractiveInputs)
            {
                kineractiveInput.Initialize();
                kineractiveInput.SetInteractiveTrigger(this);
            }
        }

        protected override void OnDisable()
        {
            foreach (KineractiveInput kineractiveInput in KineractiveInputs)
            {
                kineractiveInput.Conclude();
            }

        }
    }


}