using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Kineractive
{
    [AddComponentMenu("KINERACTIVE/Inputs/Self Activated Input")]
    public class SelfActivatedInput : KineractiveInput
    {
        public override void Initialize()
        {
            // move active hand(s) into position

            if (handSide == Handside.Left)
            {
                KineractiveManager.Instance.SetIKTarget(Handside.Left, position, moveSpeed, rotateSpeed, "left hand - self input");
            }
            else if (handSide == Handside.Right)
            {
                KineractiveManager.Instance.SetIKTarget(Handside.Right, position, moveSpeed, rotateSpeed, "right hand - self input");
            }

            KineractiveManager.Instance.HandAnimator.SetBool(inputAnimString, true);
            KineractiveManager.Instance.HandAnimator.SetBool(inputEndAnimString, false);

            OnInput.Invoke();
        }

        public override void Conclude()
        {
            base.Conclude();
            OnInputEnd.Invoke();
        }

        public override void CheckForInput()
        {
            if (BypassInput)
                return;


            if (!repeatingInput)
                return;

            OnInput.Invoke();
        }
    }

}