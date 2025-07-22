using UnityEngine;
using UnityEngine.Events;

namespace Kineractive
{
    [AddComponentMenu("KINERACTIVE/Touchables/Rotator")]
    public class Rotator : Touchable
    {
        [SerializeField] protected Vector3[] rotationsArray;
        [SerializeField] protected UnityEvent[] eventsArray;
        [SerializeField] protected AudioClip[] audioClips;
        [SerializeField] [Range(0, 1f)] protected float[] audioVolume;
        [SerializeField] protected Transform hinge;
        [SerializeField] protected bool canLoop = false;
        [SerializeField] [Range(2, 10)] protected int setsOfRotations = 2;
        [SerializeField] [Range(0, 9)] protected int rotationToStartAt = 0;

        protected int currentRotation;

        public int CurrentRotationElement
        {
            get { return currentRotation; }
        }
        
        protected virtual void Awake()
        {
            currentRotation = rotationToStartAt;

        }

        protected override void Start()
        {
            base.Start();
            RotateToElementNum(currentRotation);
        } 


        public virtual void ResetToInitialRotation()
        {
            RotateToElementNum(rotationToStartAt);
        }


        public virtual void RotationIncrement()
        {

            int newRotation = currentRotation;
            newRotation += 1;

            if (canLoop)
            {
                if (newRotation > rotationsArray.Length - 1)
                    newRotation = 0;
            }
            else
            {
                if (newRotation > rotationsArray.Length - 1)
                {
                    newRotation = rotationsArray.Length - 1;
                }
            }

            RotateToElementNum(newRotation);
        }

        public virtual void RotationDecrement()
        {

            int newRotation = currentRotation;
            newRotation -= 1;

            if (canLoop)
            {
                if (newRotation < 0)
                    newRotation = rotationsArray.Length - 1;
            }
            else
            {
                if (newRotation < 0)
                {
                    newRotation = 0;
                }
            }

            RotateToElementNum(newRotation);
        }

        public virtual void RotateToElementNum(int elementNumber)
        {
            hinge.localRotation = Quaternion.Euler(rotationsArray[elementNumber]);

            if (currentRotation == elementNumber)
                return; // if we are already in the requested position, don't do anything;

            currentRotation = elementNumber;

            eventsArray[elementNumber].Invoke();
            PlayAudioClip(audioClips[elementNumber], audioVolume[elementNumber],true);
        }

    }
    

}
