using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This heirarchy is mostly here just to keep commonality between buttons/switches etc (e.g. to find all in world buttons in the scene with one script)
/// </summary>

namespace Kineractive
{
    public abstract class Touchable : MonoBehaviour
    {
        protected AudioSource audioSource;

        protected virtual void Start()
        {
            audioSource = KineractiveManager.Instance.AudioSourceObject.GetComponent<AudioSource>();
        }


        protected virtual void PlayAudioClip(AudioClip audioClip, float volume, bool interruptCurrent = false)
        {
            if (audioClip == null)
                return;

            audioSource.clip = audioClip;
            audioSource.volume = volume;

            if (interruptCurrent)
            {
                audioSource.Play();
            }
            else if (!audioSource.isPlaying)
                audioSource.Play();
        }

        public virtual void StopAudio()
        {
            if (audioSource != null)
            {
                if (audioSource.isPlaying)
                    audioSource.Stop();
            }
        }

        public virtual void PauseAudio()
        {
            if (audioSource != null)
            {
                if (audioSource.isPlaying)
                    audioSource.Pause();
            }
        }
    }
}