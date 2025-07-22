using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kineractive
{
    [AddComponentMenu("KINERACTIVE/Inputs/Analog Input")]
    public class AnalogInput : KineractiveInput
    {
        [SerializeField] protected string axisInputString = "not set";
        [SerializeField] protected UnityEventFloat SendFloat;
        [SerializeField] protected bool invertInput;

        public override void CheckForInput()
        {
            if (BypassInput)
                return;

            float axisAmount = Input.GetAxis(axisInputString);

            if (invertInput)
                SendFloat.Invoke(axisAmount * -1);
            else
                SendFloat.Invoke(axisAmount);

            if(axisAmount != 0)
                InputActivated();

            if (axisAmount == 0)
                InputDeactivated();
        }

        protected override void InputActivated()
        {
            OnInput.Invoke();

            base.InputActivated();

            if (position != null)
                KineractiveManager.Instance.SetIKTarget(handSide, position, moveSpeed, rotateSpeed, "down action - analog axis");

            KineractiveManager.Instance.HandAnimator.SetBool(inputAnimString, true);
            KineractiveManager.Instance.HandAnimator.SetBool(inputEndAnimString, false);

            wasPressedDown = true;
        }

        protected override void InputDeactivated()
        {

            OnInputEnd.Invoke();

            base.InputDeactivated();

            if (returnPosition != null)
                KineractiveManager.Instance.SetIKTarget(handSide, returnPosition, moveSpeed, rotateSpeed, "up action - analog axis");

            KineractiveManager.Instance.HandAnimator.SetBool(inputAnimString, false); //cancel previous animation
            KineractiveManager.Instance.HandAnimator.SetBool(inputEndAnimString, true);

            wasPressedDown = false;
        }
    }
}