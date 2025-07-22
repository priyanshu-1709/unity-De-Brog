using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kineractive
{
    [AddComponentMenu("KINERACTIVE/Inputs/Axis Input")]
    public class AxisInput : KineractiveInput
    {

        [SerializeField] protected string axisInputString = "not set";
        [SerializeField] protected AxisPolarity axisPolarity;

        private float previousAxisValue;


        public override void CheckForInput()
        {
            if (BypassInput)
                return;

            switch (axisPolarity)
            {
                case AxisPolarity.Positive:
                    CheckPositiveAxisInput();
                    break;

                case AxisPolarity.Negative:
                    CheckNegativeAxisInput();
                    break;
            }

            previousAxisValue = UnityEngine.Input.GetAxisRaw(axisInputString);
        }

        void CheckNegativeAxisInput()
        {
            if(repeatingInput)
            {
                if(InputExtended.GetAxisNegative(axisInputString))
                    InputActivated();
            }
            else if (InputExtended.GetAxisDownNegative(axisInputString, previousAxisValue))
            {
                InputActivated();
            }

            if (InputExtended.GetAxisUpNegative(axisInputString, previousAxisValue))
            {
                InputDeactivated();
            }
        }

        void CheckPositiveAxisInput()
        {
            if (repeatingInput)
            {
                if (InputExtended.GetAxisPositive(axisInputString))
                    InputActivated();
            }
            else if (InputExtended.GetAxisDownPositive(axisInputString, previousAxisValue))
            {
                InputActivated();
            }

            if (InputExtended.GetAxisUpPositive(axisInputString, previousAxisValue))
            {
                InputDeactivated();
            }
        }

        protected override void InputActivated()
        {
            OnInput.Invoke();

            base.InputActivated();

            if(position != null)
                KineractiveManager.Instance.SetIKTarget(handSide, position, moveSpeed, rotateSpeed, "down action - axis");

            KineractiveManager.Instance.HandAnimator.SetBool(inputAnimString, true);
            KineractiveManager.Instance.HandAnimator.SetBool(inputEndAnimString, false);

            wasPressedDown = true;
        }

        protected override void InputDeactivated()
        {
            OnInputEnd.Invoke();

            base.InputDeactivated();

            if (returnPosition != null)
                KineractiveManager.Instance.SetIKTarget(handSide, returnPosition, moveSpeed, rotateSpeed, "up action - axis");

            KineractiveManager.Instance.HandAnimator.SetBool(inputAnimString, false); //cancel previous animation
            KineractiveManager.Instance.HandAnimator.SetBool(inputEndAnimString, true);

            wasPressedDown = false;
        }

    }
}

