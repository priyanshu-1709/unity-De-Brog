using UnityEngine;

public class ChipBoothKeyboardTrigger : MonoBehaviour
{
    public GameObject infoPanel;   // The chip info panel to show
    public AudioSource voiceover;  // The voice clip to play

    private bool hasActivated = false;

    void Update()
    {
        if (!hasActivated && Input.GetKeyDown(KeyCode.C))
        {
            if (infoPanel != null)
                infoPanel.SetActive(true);

            if (voiceover != null)
                voiceover.Play();

            hasActivated = true;
        }
    }
}
