using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Kineractive
{
    [AddComponentMenu("KINERACTIVE/Touchables/Rotator Analog")]
    public class RotatorAnalog : Touchable
    {
        protected enum StartRotation
        {
            currentRotation,
            minimumRotation,
            maximumRotation
        }

        protected enum RotationAxis
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
        [SerializeField] protected AudioClip increaseRotationSound;
        [SerializeField] protected AudioClip decreaseRotationSound;

        [SerializeField] protected float minRotation;
        [SerializeField] protected float maxRotation;
        [SerializeField] protected float rotateSpeedIncrease;
        [SerializeField] protected float rotateSpeedDecrease;
        [SerializeField] protected Transform hinge;
        [SerializeField] protected StartRotation startRotation;
        [SerializeField] protected RotationAxis rotationAxis;
        [SerializeField] protected Space coordinateSystem;
        [SerializeField] [Range(0, 1f)] protected float incVolume;
        [SerializeField] [Range(0, 1f)] protected float decVolume;

        protected Quaternion defaultLocalRotation;
        protected Quaternion defaultGlobalRotation;
        protected float currentAngle = 0f;
        protected float lastAngle = 0f;

        protected bool isOnMinRotation = false;
        protected bool isOnMaxRotation = false;

        public float CurrentAngle
        {
            get { return currentAngle; }
        }

        public Transform Hinge
        {
            get { return hinge; }
        }

        protected override void Start()
        {
            base.Start();

            defaultLocalRotation = hinge.transform.localRotation;
            defaultGlobalRotation = hinge.transform.rotation;

            switch (startRotation)
            {
                case StartRotation.minimumRotation:
                    currentAngle = minRotation;  //don't play the min event on start
                    isOnMinRotation = true;
                    SetRotation();
                    break;

                case StartRotation.maximumRotation:
                    currentAngle = maxRotation; //don't play the max event on start
                    isOnMaxRotation = true;
                    SetRotation();
                    break;
            }
    }


        public virtual void IncreaseRotation()
        {
            StopAllCoroutines();
            IncRot(1f);
        }

        public virtual void DecreaseRotation()
        {
            StopAllCoroutines();
            DecRot(1f);

        }

        public virtual void AnalogRotate(float magnitude = 1f)
        {
            StopAllCoroutines();

            if (magnitude > 0)
                IncRot(magnitude);
            else if (magnitude < 0)
                DecRot(Mathf.Abs(magnitude));
        }


        protected virtual void IncRot(float magnitude)
        {
            float scaledValue = MathfExtended.Scale(minRotation, maxRotation, 0f, 1f, currentAngle);
            dynamicFloatEvent.Invoke(scaledValue);

            if (currentAngle != lastAngle)
                PlayAudioClip(increaseRotationSound, incVolume);
            else
                audioSource.Pause();

            ChangeRotation(rotateSpeedIncrease * magnitude);
        }



        protected virtual void DecRot(float magnitude)
        {
            float scaledValue = MathfExtended.Scale(minRotation, maxRotation, 0f, 1f, currentAngle);
            dynamicFloatEvent.Invoke(scaledValue);

            if (currentAngle != lastAngle)
                PlayAudioClip(decreaseRotationSound, decVolume);
            else
                audioSource.Pause();

            ChangeRotation(-rotateSpeedDecrease * magnitude);
        }
 
        protected virtual void ChangeRotation(float rotSpeed)
        {
            float addAngle = rotSpeed * Time.deltaTime;

            lastAngle = currentAngle;
            currentAngle += addAngle;

            SetRotation();
        }

        protected virtual void SetRotation()
        {
            currentAngle = Mathf.Clamp(currentAngle, minRotation, maxRotation);
           
            if (currentAngle <= minRotation && !isOnMinRotation)
            {
                minEvent.Invoke();
                isOnMinRotation = true;
            }



            if (currentAngle >= maxRotation && !isOnMaxRotation)
            {
                maxEvent.Invoke();
                isOnMaxRotation = true;
            }


            if (currentAngle < maxRotation && isOnMaxRotation)
            {
                outOfMaxEvent.Invoke();
                isOnMaxRotation = false;
            }


            if (currentAngle > minRotation && isOnMinRotation)
            {
                outOfMinEvent.Invoke();
                isOnMinRotation = false;
            }


            switch (rotationAxis)
            {
                case RotationAxis.X:

                    if (coordinateSystem == Space.World)
                    {
                        Quaternion newRotationGlobalX = Quaternion.AngleAxis(currentAngle, hinge.transform.right);
                        hinge.transform.rotation = defaultGlobalRotation * newRotationGlobalX;

                    }

                    if (coordinateSystem == Space.Self)
                    {
                        Quaternion newRotationLocalX = Quaternion.AngleAxis(currentAngle, Vector3.right);
                        hinge.transform.localRotation = defaultLocalRotation * newRotationLocalX;
                    }

                    break;

                case RotationAxis.Y:

                    if (coordinateSystem == Space.World)
                    {
                        Quaternion newRotationGlobalY = Quaternion.AngleAxis(currentAngle, hinge.transform.up);
                        hinge.transform.rotation = defaultGlobalRotation * newRotationGlobalY;

                    }

                    if (coordinateSystem == Space.Self)
                    {
                        var newRotationLocalY = Quaternion.AngleAxis(currentAngle, Vector3.up);
                        hinge.transform.localRotation = defaultLocalRotation * newRotationLocalY;
                    }


                    break;


                case RotationAxis.Z:


                    if (coordinateSystem == Space.World)
                    {
                        var newRotationGlobalZ = Quaternion.AngleAxis(currentAngle, hinge.transform.forward);
                        hinge.transform.rotation = defaultGlobalRotation * newRotationGlobalZ;
                        
                    }
                    
                    if (coordinateSystem == Space.Self)
                    {
                        var newRotationLocalZ = Quaternion.AngleAxis(currentAngle, Vector3.forward);
                        hinge.transform.localRotation = defaultLocalRotation * newRotationLocalZ;
                    }
                    
                    break;

            }
        }

        public virtual void SetAbsoluteRotation(float absoluteValue)
        {

            lastAngle = currentAngle;

            float scaledRotation = MathfExtended.Scale(-1f, 1f, minRotation, maxRotation, absoluteValue);

            currentAngle = scaledRotation;



            SetRotation();

            float scaledValue = MathfExtended.Scale(minRotation, maxRotation, 0f, 1f, currentAngle);
            dynamicFloatEvent.Invoke(scaledValue);

            if (currentAngle != lastAngle)
            {
                if (currentAngle < lastAngle)
                    PlayAudioClip(decreaseRotationSound, decVolume);
                if (currentAngle > lastAngle)
                    PlayAudioClip(increaseRotationSound, incVolume);
            }

            if(isOnMaxRotation == true|| isOnMinRotation == true)
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
            while (currentAngle > minRotation)
            {
                DecRot(1f); 

                yield return new WaitForFixedUpdate();
            }
            audioSource.Stop();
        }

        protected virtual IEnumerator Co_ReturnToMax()
        {
            while (currentAngle < maxRotation)
            {
                IncRot(1f);

                yield return new WaitForFixedUpdate();
            }
            audioSource.Stop();
        }
    }
}