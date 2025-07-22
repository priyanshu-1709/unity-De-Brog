using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Kineractive
{
    [AddComponentMenu("KINERACTIVE/Inputs/Keycode Input")]
    public class KeycodeInput : KineractiveInput
    {
        [SerializeField] protected KeyCode keyCodeInput;


        public override void CheckForInput()
        {
            if (BypassInput)
                return;

            if (repeatingInput)
            {
                if (UnityEngine.Input.GetKey(keyCodeInput))
                    InputActivated();
            }
            else if (UnityEngine.Input.GetKeyDown(keyCodeInput))
                InputActivated();

            if (UnityEngine.Input.GetKeyUp(keyCodeInput) &&
                wasPressedDown)
                InputDeactivated();
        }


        protected override void InputActivated()
        {
            OnInput.Invoke();

            base.InputActivated();

            if (position != null)
                KineractiveManager.Instance.SetIKTarget(handSide, position, moveSpeed, rotateSpeed, "down action - keycode");
     
            KineractiveManager.Instance.HandAnimator.SetBool(inputAnimString, true);
            KineractiveManager.Instance.HandAnimator.SetBool(inputEndAnimString, false);

            wasPressedDown = true;
        }

        protected override void InputDeactivated()
        {

            OnInputEnd.Invoke();

            base.InputDeactivated();

            if (returnPosition != null)
                KineractiveManager.Instance.SetIKTarget(handSide, returnPosition, moveSpeed, rotateSpeed, "up action - keyCode");

            KineractiveManager.Instance.HandAnimator.SetBool(inputAnimString, false); //cancel previous animation
            KineractiveManager.Instance.HandAnimator.SetBool(inputEndAnimString, true);

            wasPressedDown = false; 
        }
    }
}