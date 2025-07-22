using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Kineractive
{
    public abstract class KineractiveInput : MonoBehaviour
    {

        [SerializeField] protected Handside handSide;
        [SerializeField] protected Transform position;
        [SerializeField] protected float moveSpeed = 15f;
        [SerializeField] protected float rotateSpeed = 15f;


        [SerializeField] protected bool repeatingInput = false;

        [SerializeField] protected Transform returnPosition = null;

        [SerializeField] protected string inputAnimString = "blank";
        [SerializeField] protected UnityEvent OnInput;


        [SerializeField] protected string inputEndAnimString = "blank";
        [SerializeField] protected UnityEvent OnInputEnd;

        

        [SerializeField] protected bool BypassInput = false;

        protected InputHandler interactiveTrigger;

   
        protected bool wasPressedDown; // make sure that UpAction method is not triggered on this button unless, the DownAction method was first

        public Transform ActivatePosition
        {
            get { return position; }
        }

     
        public virtual void CheckForInput()
        {
            if (BypassInput)
                return;
        }

        public virtual void Initialize()
        { }


        public virtual void Conclude()
        {
            wasPressedDown = false;

            if (KineractiveManager.Instance != null)
            {
                if (KineractiveManager.Instance.HandAnimator != null) //prevents error on game stop/exit in editor 
                {
                    foreach (AnimatorControllerParameter parameter in KineractiveManager.Instance.HandAnimator.parameters)
                    {
                        if (parameter.type == AnimatorControllerParameterType.Bool)
                        {
                            KineractiveManager.Instance.HandAnimator.SetBool(parameter.name.ToString(), false); //cancel previous animation
                        }
                    }
                }
            }
        }

        protected virtual void InputActivated()
        {
            if (interactiveTrigger.CrosshairScale != 0)
                KineractiveManager.Instance.SetCrosshairScale(interactiveTrigger.CrosshairScale);
        }

        protected virtual void InputDeactivated()
        {
            if (interactiveTrigger.CrosshairScale != 0)
                KineractiveManager.Instance.SetCrosshairScale(KineractiveManager.Instance.DefaultCrosshairScale);
        }

        public virtual void SetInteractiveTrigger(InputHandler interactiveTrigger)
        {
            this.interactiveTrigger = interactiveTrigger;
        }

        public virtual void SetBypass(bool bypassModeEnabled)
        {
            BypassInput = bypassModeEnabled;
        }

        public virtual void ToggleBypass()
        {
            BypassInput = !BypassInput;
        }
    }
}
