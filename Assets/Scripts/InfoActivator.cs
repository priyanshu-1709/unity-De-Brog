using UnityEngine;
using UnityEngine.InputSystem;

public class InfoActivator : MonoBehaviour
{
    public GameObject infoPanel;
    public AudioSource voiceover;

    private bool hasActivated = false;

    void Update()
    {
        if (Keyboard.current.mKey.wasPressedThisFrame && !hasActivated)
        {
            if (infoPanel != null)
                infoPanel.SetActive(true);

            if (voiceover != null)
                voiceover.Play();

            hasActivated = true;
        }
    }
}
