using System.Collections;
using System.Collections.Generic;
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


    [AddComponentMenu("KINERACTIVE/Handler/Input Handler")]
    public class InputHandler : MonoBehaviour
    {
 
        [SerializeField] protected string usageInstructions = "N/A";
        [SerializeField] protected Texture controlsIconTexture;
        [SerializeField] protected Texture crosshairTexture;
        [SerializeField][Range(0f,5f)] protected float crosshairScale = 1.5f;
        [SerializeField] protected float maxInteractionRange = 1f;

        protected Texture previousCrosshair;

        [SerializeField] protected KineractiveInput[] KineractiveInputs;


        public KineractiveInput[] AllInteractions
        {
            get { return KineractiveInputs; }
        }

        public float CrosshairScale
        {
            get { return crosshairScale; }
        }

        public float MaxInteractionRange
        {
            get { return maxInteractionRange; }
        }

            
        protected virtual void LateUpdate()
        {
            if(KineractiveInputs.Length == 0)
            {
                Debug.LogError("Inputs not set in Input Handler: " + gameObject.name + " " + transform.parent.name);
                return;
            }

            foreach (KineractiveInput kineractiveInput in KineractiveInputs)
            {
                if (kineractiveInput == null)
                {
                    Debug.LogError("Inputs not set in Input Handler: " + gameObject.name + " " + transform.parent.name);
                    return;
                }

                kineractiveInput.CheckForInput();
            }
        }

        protected virtual void OnEnable()
        {
            KineractiveManager.Instance.BackgroundImage.enabled = true;                 //enable instructions pop up
            KineractiveManager.Instance.InteractionText.text = usageInstructions;
            KineractiveManager.Instance.InteractionText.enabled = true;
            KineractiveManager.Instance.ControlsIcon.texture = controlsIconTexture;
            KineractiveManager.Instance.ControlsIcon.enabled = true;

            previousCrosshair = KineractiveManager.Instance.Crosshair.texture;   //remember the previous cursor so we can set it back afterwards
            KineractiveManager.Instance.Crosshair.texture = crosshairTexture;

            KineractiveManager.Instance.AudioSourceObject.transform.position = transform.position; // move 3D audio source to this interactive so it sounds like sound is coming from it
         
            foreach (KineractiveInput kineractiveInput in KineractiveInputs)
            {
                kineractiveInput.Initialize();
                kineractiveInput.SetInteractiveTrigger(this);
            }
        }

        protected virtual void OnDisable()
        {
            if(KineractiveManager.Instance != null)
            {
                if (KineractiveManager.Instance.BackgroundImage != null)
                    KineractiveManager.Instance.BackgroundImage.enabled = false;   //disable usage instructions pop and return cursor back to what it was originally

                if(KineractiveManager.Instance.InteractionText != null)
                   KineractiveManager.Instance.InteractionText.enabled = false;

                if(KineractiveManager.Instance.ControlsIcon != null)
                   KineractiveManager.Instance.ControlsIcon.enabled = false;

                if(KineractiveManager.Instance.Crosshair != null)
                   KineractiveManager.Instance.Crosshair.texture = previousCrosshair;
            }


            foreach (KineractiveInput kineractiveInput in KineractiveInputs)
            {
                kineractiveInput.Conclude();
            }
        }

        public void SetUsageInstructions(string instructions)
        {
            usageInstructions = instructions;
            KineractiveManager.Instance.InteractionText.text = usageInstructions;
        }

        public void  SetControlsIcon(Texture controlsTexture)
        {
            controlsIconTexture = controlsTexture;
            KineractiveManager.Instance.ControlsIcon.texture = controlsIconTexture;
        }

        public void SetCrosshairIcon(Texture crosshairTexture)
        {
            this.crosshairTexture = crosshairTexture;
            KineractiveManager.Instance.Crosshair.texture = this.crosshairTexture;
        }
    }
}