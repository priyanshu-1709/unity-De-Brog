using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Kineractive
{
    [AddComponentMenu("KINERACTIVE/Touchables/ButtonTouchable")]
    public class ButtonTouchable : Touchable
    {

        public enum ButtonState
        {
            Pressed,
            In,
            Out
        }

        [SerializeField] protected AudioClip pressedClip;
        [SerializeField] protected AudioClip inClip;
        [SerializeField] protected AudioClip outClip;

        [SerializeField] protected Vector3 inPosition;
        [SerializeField] protected Vector3 outPosition;
        [SerializeField] protected Vector3 pressedPosition;

        [SerializeField] protected Transform theButton;

        [SerializeField] protected bool startPushedIn;
        [SerializeField] protected bool buttonIsToggle;

        [SerializeField] protected UnityEvent onPress;
        [SerializeField] protected UnityEvent onInPosition;
        [SerializeField] protected UnityEvent onOutPosition;

        [SerializeField] [Range(0, 1f)] protected float pressedClipVolume = 1f;
        [SerializeField] [Range(0, 1f)] protected float inClipVolume = 1f;
        [SerializeField] [Range(0, 1f)] protected float outClipVolume = 1f;

        protected ButtonState buttonState;
        protected ButtonState previousButtonState;

        public ButtonState Button_State
        {
            get { return buttonState; }
        }
      
        protected bool hasPlayedClickOnce = false; //don't play the click sound over and over if button is held down

        protected override void Start()
        {
            base.Start();

            if (startPushedIn)
                ChangeButtonState(ButtonState.In);
            else
                ChangeButtonState(ButtonState.Out);
        }

        public virtual void PressButton()
        {
            previousButtonState = buttonState;
            ButtonPressed();
        }

        public virtual void ReleaseButton()
        {

            if(buttonIsToggle)
            {
                ToggleButton();
            }
            else
            {
                ButtonOut();
            }
        }

        public virtual void UnClickButton()
        {
            if(buttonState == ButtonState.Pressed)
                ReleaseButton();
        }

        protected virtual void ToggleButton()
        {
          if(previousButtonState == ButtonState.In)
          {
                ButtonOut();
          }
          else if(previousButtonState == ButtonState.Out)
          {
                ButtonIn();
          }
        
        }


        protected virtual void ButtonPressed()
        {
            onPress.Invoke();
            ChangeButtonState(ButtonState.Pressed);
            if (!hasPlayedClickOnce) 
            {
                PlayAudioClip(pressedClip, pressedClipVolume, true);
                hasPlayedClickOnce = true;
            }
        }


        protected virtual void ButtonIn()
        {
            onInPosition.Invoke();     
            ChangeButtonState(ButtonState.In);
            PlayAudioClip(inClip, inClipVolume, true);
            hasPlayedClickOnce = false;

        }

        protected virtual void ButtonOut()
        {
            onOutPosition.Invoke();
            ChangeButtonState(ButtonState.Out);
            PlayAudioClip(outClip, outClipVolume, true);
            hasPlayedClickOnce = false;
        }

        protected virtual void ChangeButtonState(ButtonState newButtonState)
        {
            buttonState = newButtonState;

            switch (buttonState)
            {
                case ButtonState.Pressed:
                    theButton.localPosition = pressedPosition;
                    break;

                case ButtonState.In:
                    theButton.localPosition = inPosition;
                    break;

                case ButtonState.Out:
                    theButton.transform.localPosition = outPosition;
                    break;
            }
        }

    }
}
