using UnityEngine;
using TMPro;

public class ParticleSelector : MonoBehaviour
{
    [Header("Particle Info")]
    public string particleName;           // Example: "Electron"
    public float mass;                    // Example: 9.1e-31
    public float velocity = 1e6f;         // Example: 1,000,000 m/s

    [Header("UI Elements")]
    public TMP_Text nameText;             // Title (e.g., Electron)
    public TMP_Text resultText;           // Displays mass, velocity, λ

    private float h = 6.626e-34f;         // Planck's constant

    private AudioSource voiceoverSource;

    void Start()
    {
        // Get attached AudioSource
        voiceoverSource = GetComponent<AudioSource>();
    }

    public void ShowWavelength()
    {
        // Calculate λ = h / mv
        float lambda = h / (mass * velocity);

        // Set name display
        if (nameText != null)
        {
            nameText.text = particleName;
        }

        // Format and display results
        if (resultText != null)
        {
            resultText.text =
                $"<b>Mass:</b> {mass:e2} kg\n" +
                $"<b>Velocity:</b> {velocity:e2} m/s\n" +
                $"<b>λ (De Broglie):</b> {lambda:e2} meters";
        }

        // Play voiceover
        if (voiceoverSource != null && voiceoverSource.clip != null)
        {
            voiceoverSource.Stop(); // Ensure no overlap
            voiceoverSource.Play();
        }
    }
}
