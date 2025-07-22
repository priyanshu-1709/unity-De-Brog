using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kineractive
{
    [AddComponentMenu("KINERACTIVE/Entities/Audio Clip Player")]
    [RequireComponent(typeof(AudioSource))]
    public class Ent_AudioClipPlayer : MonoBehaviour
    {
        [SerializeField] protected AudioClip audioClip;
        [SerializeField] protected bool playOnStart;
        [SerializeField] protected bool interruptCurrentClip;
        [SerializeField] protected float volumeChangeAmount;

        [SerializeField] protected float pitchChangeAmount;
        [SerializeField] protected float minPitch;
        [SerializeField] protected float maxPitch;

        protected AudioSource audioSource;


        protected virtual void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = audioClip;

            if (playOnStart)
                audioSource.Play();
        }

        public virtual void PlayAudioClip()
        {
            if (interruptCurrentClip)
                audioSource.Play();
            else
            {
                if (!audioSource.isPlaying)
                    audioSource.Play();
            }
        }

        public virtual void StopAudioClip()
        {
            audioSource.Stop();
        }

        public virtual void PauseAudioClip()
        {
            audioSource.Pause();
        }

        public virtual void AdjustVolumeScaled(float newVolume)
        {
            audioSource.volume = newVolume;
        }

        public virtual void VolumeUp()
        {
            audioSource.volume += volumeChangeAmount * Time.deltaTime;
        }

        public virtual void VolumeDown()
        {
            audioSource.volume -= volumeChangeAmount * Time.deltaTime;
        }

        public virtual void PitchUp()
        {
            audioSource.pitch += pitchChangeAmount * Time.deltaTime;

            if (audioSource.pitch > maxPitch)
                audioSource.pitch = maxPitch;
        }

        public virtual void PitchDown()
        {
            audioSource.pitch -= pitchChangeAmount * Time.deltaTime;

            if (audioSource.pitch < minPitch)
                audioSource.pitch = minPitch;
        }
    }
}