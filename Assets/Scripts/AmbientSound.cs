using UnityEngine;

public class AmbientSoundTrigger : MonoBehaviour
{
    public AudioSource ambientAudio;

    public void PlayAmbient()
    {
        if (!ambientAudio.isPlaying)
        {
            ambientAudio.Play();
        }
    }
}
