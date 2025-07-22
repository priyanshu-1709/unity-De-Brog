using UnityEngine;
using TMPro;

public class NanoDeviceFixer : MonoBehaviour
{
    [Header("UI")]
    public TMP_InputField massInput;
    public TMP_InputField velocityInput;
    public TMP_Text resultText;

    [Header("Success Feedback")]
    public GameObject successEffect;
    public AudioSource successAudio;

    [Header("Broken Effects")]
    public ParticleSystem smokeEffect;
    public ParticleSystem sparkEffect;

    [Header("Environment Sound")]
    public AudioSource beepSound;

    private double h = 6.626e-34;
    private double requiredLambda = 1e-10;
    private bool isFixed = false;

    void Start()
    {
        // Make sure beep doesn't auto-play
        if (beepSound != null && beepSound.isPlaying)
        {
            beepSound.Stop();
        }
    }

    /// <summary>
    /// Call this once when device is "broken" to start beeping.
    /// </summary>
    public void StartEmergencyBeep()
    {
        if (!isFixed && beepSound != null && !beepSound.isPlaying)
        {
            beepSound.loop = true;
            beepSound.Play();
        }
    }

    /// <summary>
    /// Call this from the Calculate button
    /// </summary>
    public void CalculateWavelength()
    {
        if (isFixed)
        {
            resultText.text = "✅ Nano-device is already fixed!";
            return;
        }

        // Try parsing inputs
        bool validMass = double.TryParse(massInput.text, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out double m);
        bool validVelocity = double.TryParse(velocityInput.text, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out double v);

        if (!validMass || !validVelocity || m <= 0 || v <= 0)
        {
            resultText.text = "❌ Please enter valid positive numbers.\nExample mass: 9.11e-31\nExample velocity: 10e7";
            return;
        }

        // Calculate wavelength
        double lambda = h / (m * v);
        resultText.text = $"λ = {lambda:e2} m";

        if (lambda < requiredLambda)
        {
            // Mark fixed forever
            isFixed = true;
            resultText.text += "\n✅ Nano-device fixed!";

            // Play success effects
            if (successEffect != null) successEffect.SetActive(true);
            if (successAudio != null) successAudio.Play();

            // Stop broken VFX
            if (smokeEffect != null && smokeEffect.isPlaying) smokeEffect.Stop();
            if (sparkEffect != null && sparkEffect.isPlaying) sparkEffect.Stop();

            // STOP BEEP FOREVER
            if (beepSound != null && beepSound.isPlaying)
            {
                beepSound.Stop();
            }
        }
        else
        {
            // STILL BROKEN
            resultText.text += "\n❌ λ is too large. Try again.";

            // Only play beep if not fixed
            if (!isFixed && beepSound != null && !beepSound.isPlaying)
            {
                beepSound.loop = true;
                beepSound.Play();
            }
        }
    }
}
