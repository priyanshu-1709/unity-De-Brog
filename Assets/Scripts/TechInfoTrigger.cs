using UnityEngine;

public class TriggerTester : MonoBehaviour
{
    public GameObject panel;
    public AudioSource voiceover;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PLAYER ENTERED!");
            if (panel != null) panel.SetActive(true);
            if (voiceover != null) voiceover.Play();
        }
    }
}
