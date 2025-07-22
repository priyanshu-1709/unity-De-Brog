using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kineractive
{
    [AddComponentMenu("KINERACTIVE/Entities/Light")]
    public class Ent_Light : MonoBehaviour
    {
        [SerializeField] protected float minIntensity;
        [SerializeField] protected float maxIntensity;
        [SerializeField] protected float intensityStep;
        [SerializeField] protected float rangeStep;
        [SerializeField] protected float minRange;
        [SerializeField] protected float maxRange;


        protected Light lightComponent;
        protected bool isFadingOut = false;
        protected bool isFadingIn = false;

        protected void Start()
        {
            lightComponent = GetComponent<Light>();
        }

        public virtual void TurnOn()
        {
            lightComponent.enabled = true;
        }

        public virtual void TurnOff()
        {
            lightComponent.enabled = false;
        }

        public virtual void Toggle()
        {
            if (lightComponent.enabled == true)
                TurnOff();
            else if (lightComponent.enabled == false)
                TurnOn();
        }

        public virtual void IncreaseIntensity()
        {
            lightComponent.intensity += intensityStep;
        }

        public virtual void DecreaseIntensity()
        {
            lightComponent.intensity -= intensityStep;
        }

        public virtual void IncreaseRange()
        {
            lightComponent.range += rangeStep;

            if (lightComponent.range > maxRange)
                lightComponent.range = maxRange;
        }

        public virtual void DecreaseRange()
        {
            lightComponent.range -= rangeStep;

            if (lightComponent.range < minRange)
                lightComponent.range = minRange;
        }

        public virtual void SetIntensity(float newIntensity)
        {
            lightComponent.intensity = newIntensity;
        }

        public virtual void SetRange(float newRange)
        {
            lightComponent.range = newRange;
        }

        public virtual void AdjustIntensityScaled(float newIntensity)
        {
            float scaledIntensity = MathfExtended.Scale(0f, 1f, minIntensity, maxIntensity, newIntensity);
            lightComponent.intensity = scaledIntensity;
        }

        public virtual void AdjustRangeScaled(float newRange)
        {
            float scaledRanged = MathfExtended.Scale(0f, 1f, minRange, maxRange, newRange);
            lightComponent.range = scaledRanged;
        }


        public virtual void FadeOut()
        {
            StopCoroutine(FadeInCoroutine());
            isFadingIn = false;

            if (isFadingOut)
                return;

            StartCoroutine(FadeOutCoroutine());

        }

        public virtual void FadeIn()
        {
            StopCoroutine(FadeOutCoroutine());
            isFadingOut = false;

            if (isFadingIn)
                return;

            StartCoroutine(FadeInCoroutine());
        }

        protected IEnumerator FadeOutCoroutine()
        {
            isFadingOut = true;

            while (lightComponent.intensity > minIntensity)
            {
                lightComponent.intensity -= intensityStep * Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            isFadingOut = false;
        }

        protected IEnumerator FadeInCoroutine()
        {
            isFadingIn = true;

            while (lightComponent.intensity < maxIntensity)
            {
                lightComponent.intensity += intensityStep * Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            isFadingIn = false;
        }
    }

}