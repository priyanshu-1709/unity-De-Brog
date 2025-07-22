using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This script changes the Inpsector view for BinaryInput to make the serialized Input field appear at the top of the script in the inspector
/// </summary>

namespace Kineractive
{
    [AddComponentMenu("KINERACTIVE/Inputs/Button Input")]
    public class ButtonInput : KineractiveInput
    {

        [SerializeField] protected string buttonInputString = "not set";


        public override void CheckForInput()
        {
            if (BypassInput)
                return;

            if (repeatingInput)
            {
                if (UnityEngine.Input.GetButton(buttonInputString))
                    InputActivated();
            }
            else if (UnityEngine.Input.GetButtonDown(buttonInputString))
                InputActivated();

            if (UnityEngine.Input.GetButtonUp(buttonInputString) &&
                wasPressedDown)
                InputDeactivated();
        }


        protected override void InputActivated()
        {
            OnInput.Invoke();

            base.InputActivated();

            if (position != null)
                KineractiveManager.Instance.SetIKTarget(handSide, position, moveSpeed, rotateSpeed, "down action - button");
     
            KineractiveManager.Instance.HandAnimator.SetBool(inputAnimString, true);
            KineractiveManager.Instance.HandAnimator.SetBool(inputEndAnimString, false);

            wasPressedDown = true;
        }

        protected override void InputDeactivated()
        {

            OnInputEnd.Invoke();

            base.InputDeactivated();

            if (returnPosition != null)
                KineractiveManager.Instance.SetIKTarget(handSide, returnPosition, moveSpeed, rotateSpeed, "up action - button");

            KineractiveManager.Instance.HandAnimator.SetBool(inputAnimString, false); //cancel previous animation
            KineractiveManager.Instance.HandAnimator.SetBool(inputEndAnimString, true);

            wasPressedDown = false; 
        }
    }
}