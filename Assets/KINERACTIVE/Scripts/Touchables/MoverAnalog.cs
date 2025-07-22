using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Kineractive
{
    [AddComponentMenu("KINERACTIVE/Touchables/Mover Analog")]
    public class MoverAnalog : Touchable
    {
        protected enum StartPosition
        {
            currentPosition,
            minimumPosition,
            maximumPosition
        }

        protected enum MoveAxis
        {
            X,
            Y,
            Z
        }

        [SerializeField] protected UnityEventFloat dynamicFloatEvent;
        [SerializeField] protected UnityEvent minEvent;
        [SerializeField] protected UnityEvent maxEvent;
        [SerializeField] protected UnityEvent outOfMinEvent;
        [SerializeField] protected UnityEvent outOfMaxEvent;
        [SerializeField] protected AudioClip increasePositionSound;
        [SerializeField] protected AudioClip decreasePositionSound;

        [SerializeField] protected float minPosition;
        [SerializeField] protected float maxPosition;
        [SerializeField] protected float moveSpeedIncrease;
        [SerializeField] protected float moveSpeedDecrease;
        [SerializeField] protected Transform objectToMove;
        [SerializeField] protected StartPosition startPosition;
        [SerializeField] protected MoveAxis moveAxis;

        [SerializeField] [Range(0, 1f)] protected float incVolume;
        [SerializeField] [Range(0, 1f)] protected float decVolume;

        protected Vector3 defaultPosition;
        protected float currentPos = 0f;
        protected float lastPos = 0f;

        protected bool isOnMinPosition = false;
        protected bool isOnMaxPosition = false;

        public float CurrentPosition
        {
            get { return currentPos; }
        }

        public Transform ObjectMoved
        {
            get { return objectToMove; }
        }

        protected override void Start()
        {
            base.Start();

            defaultPosition = objectToMove.transform.localPosition;

            switch (startPosition)
            {
                case StartPosition.minimumPosition:
                    currentPos = minPosition;
                    isOnMinPosition = true;  //don't invoke the min event on start
                    SetPosition();
                    break;

                case StartPosition.maximumPosition:
                    currentPos = maxPosition;
                    isOnMaxPosition = true; //don't invoke the max event on start
                    SetPosition();
                    break;
            }
        }


        public virtual void IncreasePosition()
        {
            StopAllCoroutines();
            IncPos(1f);
        }

        public virtual void DecreasePosition()
        {
            StopAllCoroutines();
            DecPos(1f);

        }

        public virtual void AnalogMove(float magnitude = 1f)
        {
            StopAllCoroutines();

            if (magnitude > 0)
                IncPos(magnitude);
            else if (magnitude < 0)
                DecPos(Mathf.Abs(magnitude));
        }

        protected virtual void IncPos(float magnitude)
        {
            float scaledValue = MathfExtended.Scale(minPosition, maxPosition, 0f, 1f, currentPos);
            dynamicFloatEvent.Invoke(scaledValue);

            if (currentPos != lastPos)
                PlayAudioClip(increasePositionSound, incVolume);
            else
                audioSource.Pause();


            ChangePosition(moveSpeedIncrease * magnitude);
        }




        protected virtual void DecPos(float magnitude)
        {
    
            float scaledValue = MathfExtended.Scale(minPosition, maxPosition, 0f, 1f, currentPos);
            dynamicFloatEvent.Invoke(scaledValue);
            
            if (currentPos != lastPos)
                PlayAudioClip(decreasePositionSound, decVolume);
            else
                audioSource.Pause();


            ChangePosition(-moveSpeedDecrease * magnitude);
        }

        protected virtual void ChangePosition(float moveSpeed)
        {

            float addPos = moveSpeed * Time.deltaTime;

            lastPos = currentPos;
            currentPos += addPos;
            SetPosition();
        }

        protected virtual void SetPosition()
        {

            currentPos = Mathf.Clamp(currentPos, minPosition, maxPosition);


            if (currentPos <= minPosition && !isOnMinPosition)
            {
                minEvent.Invoke();
                isOnMinPosition = true;
            }


            if (currentPos >= maxPosition && !isOnMaxPosition)
            {
                maxEvent.Invoke();
                isOnMaxPosition = true;
            }


            if (currentPos < maxPosition && isOnMaxPosition)
            {
                outOfMaxEvent.Invoke();
                isOnMaxPosition = false;
            }


            if (currentPos > minPosition && isOnMinPosition)
            {
                outOfMinEvent.Invoke();
                isOnMinPosition = false;
            }


            switch (moveAxis)
            {
                case MoveAxis.X:
                    objectToMove.transform.localPosition = defaultPosition + Vector3.right * currentPos;
                    break;

                case MoveAxis.Y:
                    objectToMove.transform.localPosition = defaultPosition + Vector3.up * currentPos;
                    break;

                case MoveAxis.Z:
                    objectToMove.transform.localPosition = defaultPosition + Vector3.forward * currentPos;
                    break;

            }
        }

        public virtual void SetAbsolutePosition(float absoluteValue)
        {
            lastPos = currentPos;

            float scaledPosition = MathfExtended.Scale(-1f, 1f, minPosition, maxPosition, absoluteValue);

            currentPos = scaledPosition;
            
            SetPosition();

            float scaledValue = MathfExtended.Scale(minPosition, maxPosition, 0f, 1f, currentPos);
            dynamicFloatEvent.Invoke(scaledValue);

            if (currentPos != lastPos)
            {
                if (currentPos < lastPos)
                    PlayAudioClip(decreasePositionSound, decVolume);
                if (currentPos > lastPos)
                    PlayAudioClip(increasePositionSound, incVolume);
            }

            if (isOnMaxPosition == true || isOnMinPosition == true)
            {
                audioSource.Pause();
            }
        }

        public virtual void ReturnToMin()
        {
            StopAllCoroutines();
            StartCoroutine(Co_ReturnToMin());
        }

        public virtual void ReturnToMax()
        {
            StopAllCoroutines();
            StartCoroutine(Co_ReturnToMax());
        }

        protected virtual IEnumerator Co_ReturnToMin()
        {
            while (currentPos > minPosition)
            {
                DecPos(1f);

                yield return new WaitForFixedUpdate();
            }
            audioSource.Stop();
        }

        protected virtual IEnumerator Co_ReturnToMax()
        {
            while (currentPos < maxPosition)
            {
                IncPos(1f);

                yield return new WaitForFixedUpdate();
            }
            audioSource.Stop();
        }
    }
}