using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Kineractive
{
    [AddComponentMenu("KINERACTIVE/Touchables/Mover")]
    public class Mover : Touchable
    {
        [SerializeField] protected Vector3[] positionsArray;
        [SerializeField] protected UnityEvent[] eventsArray;
        [SerializeField] protected AudioClip[] audioClips;
        [SerializeField] [Range(0, 1f)] protected float[] audioVolume;
        [SerializeField] protected Transform movableObject;
        [SerializeField] protected bool canLoop = false;
        [SerializeField] [Range(2, 10)] protected int setsOfPositions = 2;
        [SerializeField] [Range(0, 9)] protected int positionToStartAt = 0;

        protected int currentPosition;

        public int CurrentPositionElement
        {
            get { return currentPosition; }
        }

        protected virtual void Awake()
        {
            currentPosition = positionToStartAt;
        }

        protected override void Start()
        {
            base.Start();
            MoveToElementNum(currentPosition);
                       
        }


        public virtual void ResetToInitialPosition()
        {
            MoveToElementNum(positionToStartAt);
        }


        public virtual void PositionIncrement()
        {

            int newPosition = currentPosition;
            newPosition += 1;

            if (canLoop)
            {
                if (newPosition > positionsArray.Length - 1)
                    newPosition = 0;
            }
            else
            {
                if (newPosition > positionsArray.Length - 1)
                {
                    newPosition = positionsArray.Length - 1;
                }
            }

            MoveToElementNum(newPosition);
        }

        public virtual void PositionDecrement()
        {

            int newPosition = currentPosition;
            newPosition -= 1;

            if (canLoop)
            {
                if (newPosition < 0)
                    newPosition = positionsArray.Length - 1;
            }
            else
            {
                if (newPosition < 0)
                {
                    newPosition = 0;
                }
            }

            MoveToElementNum(newPosition);
        }

        public virtual void MoveToElementNum(int elementNumber)
        {
            movableObject.localPosition = positionsArray[elementNumber];

            if (currentPosition == elementNumber)
                return; // if we are already in the requested position, don't do anything;

            currentPosition = elementNumber;

            eventsArray[elementNumber].Invoke();
            PlayAudioClip(audioClips[elementNumber],audioVolume[elementNumber], true);
        }

    }


}
