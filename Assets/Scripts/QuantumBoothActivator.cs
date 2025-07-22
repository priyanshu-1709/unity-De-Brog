using UnityEngine;
using UnityEngine.InputSystem;

public class QuantumBoothActivator : MonoBehaviour
{
    public GameObject infoPanel;     // Assign your quantum research UI panel here
    public AudioSource voiceover;    // Assign your voiceover audio clip here

    private bool hasActivated = false;

    void Update()
    {
        if (!hasActivated && Keyboard.current.qKey.wasPressedThisFrame)
        {
            if (infoPanel != null)
                infoPanel.SetActive(true);

            if (voiceover != null)
                voiceover.Play();

            hasActivated = true;
        }
    }
}
